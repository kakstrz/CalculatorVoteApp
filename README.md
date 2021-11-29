Instrukcja konfiguracji środowiska
1. Uruchom instancję sql server express 2019
2. W Konsoli Managera Pakietów należy wpisać i uruchomić "update-database".
3. Dla Solucji trzeba zrobić restore pakietów NuGet.

Funkcjonalności do dorobienia
1. W projekcie Calculator.Data znajdują się zahardcodowane dane kandydatów i parti - w tym samym 
   projekcie stworzyłam CandidateProvider, którego zadaniem miało być wykorzystanie serwisu CandidatesService
   do pobrania z udostępnionego API kadydatów oraz partii. Następnie na podstawie pobranych danych
   wykonanie odpowiednich operacji na bazie danych. Nie zdążyłam zaimplementować części wykorzytującej
   tę usługę.
2. Obsługa eksportu aktualnego stanu głosów do pliku CSV oraz PDF. Należy stworzyć generyczny interfejs do
   eksportu plików różnych rozszerzeń.
3. Przeniesienie stałych tekstów do Resources.resx.
4. Przeniesienie connection string bazy do pliku secret.json.
5. Dodanie walidacji dla formularza logowanie.