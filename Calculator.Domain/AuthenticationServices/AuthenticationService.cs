using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Calculator.Domain.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IGenericRepository<UnauthorizedAttempt> _unauthorizedAttemptRepository;
        public AuthenticationService(
            IUserRepository userRepository,
            IUserService userService,
            IGenericRepository<UnauthorizedAttempt> unauthorizedAttemptRepository)
        {
            _userRepository = userRepository;
            _userService = userService;
            _unauthorizedAttemptRepository = unauthorizedAttemptRepository;
        }

        public async Task<User> Login(string name, string surename, string pesel)
        {
            bool hasVoteRight = await CheckUserVoteRights(pesel) &&
                CheckUserLeagalAge(pesel) &&
                CountPeselChecksum(pesel);
             
            var users = await _userRepository.GetUsers(name, surename);

            User loggingUser = null;

            foreach (var user in users)
            {
                CreateHash(pesel, out byte[] peselHash, user.PeselSalt);
                if (peselHash.SequenceEqual(user.PeselHash))
                {
                    loggingUser = user;
                    break;
                }
            }

            if (loggingUser == null)
            {
                CreateSalt(pesel, out byte[] peselSalt);
                CreateHash(pesel, out byte[] peselHash, peselSalt);

                loggingUser = new User()
                {
                    Name = name,
                    SureName = surename,
                    HasVoted = false,
                    HasVoteRight = hasVoteRight,
                    PeselSalt = peselSalt,
                    PeselHash = peselHash
                };
                await _userRepository.Create(loggingUser);
            }

            return loggingUser;
        }

        private bool CheckUserLeagalAge(string pesel)
        {
            if (pesel.Length != 11)
                return false;

            int[] dateOfBirth = new int[3];

            dateOfBirth[0] = Int32.Parse(pesel[0].ToString()) * 10 + Int32.Parse(pesel[1].ToString());
            dateOfBirth[1] = Int32.Parse(pesel[2].ToString()) * 10 + Int32.Parse(pesel[3].ToString());
            dateOfBirth[2] = Int32.Parse(pesel[4].ToString()) * 10 + Int32.Parse(pesel[5].ToString());

            if (CheckUserDateOfBirth(dateOfBirth))
                return true;

            return false;
        }

        private bool CheckUserDateOfBirth(int[] dateOfBirth)
        {
            DateTime electionDate = DateTime.Now;

            int year;
            int month;
            int day = dateOfBirth[2];

            if (dateOfBirth[1] <= 12)
            {
                //born in XX century
                year = 1900 + dateOfBirth[0];
                month = dateOfBirth[1];
            }
            else
            {
                //born in XXI century
                year = 2000 + dateOfBirth[0];
                month = dateOfBirth[1];
                if (dateOfBirth[1] > 20 && dateOfBirth[1] < 30) month -= 10;
                if (dateOfBirth[1] > 30) month -= 20;
            }

            if (electionDate.Year - year > 18)
                return true;

            if (electionDate.Year - year == 18 && electionDate.Month - month > 0)
                return true;

            if (electionDate.Year - year == 18 && electionDate.Month - month == 0 && electionDate.Day - day >= 0)
                return true;

            return false;
        }

        private static bool CountPeselChecksum(string pesel)
        {
            int[] peselMultiplier = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int sum = 0;
            for (int i = 0; i < peselMultiplier.Length; i++)
            {
                sum += peselMultiplier[i] * int.Parse(pesel[i].ToString());
            }

            int rest = sum % 10;
            return (rest == 0 ? rest : (10 - rest)).Equals(int.Parse(pesel[10].ToString()));
        }

        private async Task<bool> CheckUserVoteRights(string pesel)
        {
            bool hasVoteRights = true;
            var blockedUsers = await _userService.GetDisallowedUsers(Models.IndexTypes.UsersIndexType.Blocked);

            foreach(var blockedUser in blockedUsers.Disallowed.Person)
            {
                if(blockedUser.Pesel == pesel)
                {
                    hasVoteRights = false;
                    await _unauthorizedAttemptRepository.Create(new UnauthorizedAttempt { Date = DateTime.Now });
                }
            }

            return hasVoteRights;
        }

        private void CreateSalt(string pesel, out byte[] peselSalt)
        {
            peselSalt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(peselSalt);
            }
        }

        private void CreateHash(string pesel, out byte[] peselHash, byte[] peselSalt)
        {
            peselHash = KeyDerivation.Pbkdf2(
            password: pesel,
            salt: peselSalt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 100000,
            numBytesRequested: 256 / 8);
        }
    }
}
