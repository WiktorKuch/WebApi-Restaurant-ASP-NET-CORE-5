# WebApi-Restaurant-ASP-NET-CORE-5

Aplikacja web API stworzona za pomocą ASP.NET Core 5  docelowo może zostać wykorzystana przez SPA ( single page application) aplikacje mobilne, aplikacje IoT lub dowolną aplikację, w której komunikacja odbywa się za pośrednictwem protokołu HTTP.W aplikacji zostały uwzględnione dobre praktyki takie jak wstrzykiwanie zależności, automatyczne mapowanie, rejestrowanie błędów, walidacja modelu,  mapowanie relacyjno-obiektowe - Entity Framework.
Aplikacja została stworzona zgodnie z architekturą REST - czyta, tworzy, modyfikuje lub usuwa dane z serwera. Wysłanie zapytań do bazy danych z kodu za pomocą ORM : Entity Framework Core.Utworzona baza danych MS SQL w oparciu o kod w C#. Walidacja przychodzących modeli  i zwrot odpowiednich komunikatów w przypadku nieprawidłowości. Automatyczne mapowanie.Użycie wbudowanego kontenera w celu wstrzyknięcia zależności. Rejestracja błędów lub określonych informacji w pliku tekstowym. Utworzona dokumentacja za pomocą narzędzia Swagger.
Konfiguracja NLogger. Użycie Postmana aby korzystać z internetowego interfejsu API. Autentykacja użytkowników za pomocą tokenów JWT. Własne zasady polityki i  autoryzacji. Autoryzacja rolą użytkownika oraz wartością claim’u.
Hasła użytkowników zabezpieczone za pomocą hashy. Implementacja własnych Middleware.Obsługa prywatnych i publicznych plików statycznych - zwracanie ich z API oraz przesyłanie ich na serwer. Paginacja zwracanych wyników. Konfiguracja polityki CORS. Cache odpowiedzi.

Aplikacja zwiera testy jednostkowe w  xUnit z użyciem Moq i FluentAssertions. Testy integracyjne korzystają z WebApplicationFactoryBuilder. Mockowanie bazy danych jako EntityFramework InMemoryDataBase.
Współdzielony kontekst testów. Baza danych w pamięci. Testowanie zapytania wymagającego uwierzytelnienia -<FakePolicyEvaluator , IPolicyEvaluator>, <FakeUserFilter , IAsyncActionFilter> . Testowanie walidacji zapytania. Seedowanie danych testowych. Testowanie rejestracji  i logowania użytkowników. Mockowanie serwisów. Testowanie walidatorów oraz rejestracji zależności.







The web API application created with ASP.NET Core 5 can ultimately be used by SPA (single page application) mobile applications, IoT applications or any application in which communication takes place via the HTTP protocol.
The application includes good practices such as dependency injection, automatic mapping, error logging, model validation, object-relational mapping - Entity Framework.The application was created in accordance with the REST architecture - it reads, creates, modifies or deletes data from the server. Sending database queries from code using ORM : Entity Framework Core.Created MS SQL database based on C# code. Validation of incoming models and return of appropriate messages in case of irregularities. Automatic mapping.Using a built-in container to inject dependencies. Logging of errors or specific information in a text file. Documentation created using Swagger.NLogger configuration. Using Postman to use the web API.User authentication using JWT tokens. Your own policy and authorization rules.Authorization with the user role and claim value.User passwords secured with hashes. Implementation of own Middleware. Support for private and public static files - returning them from the API and uploading them to the server. Pagination of returned results. CORS policy configuration. Reply cache.

The application includes unit tests in xUnit using Moq and FluentAssertions. Integration tests use WebApplicationFactoryBuilder. Mocking the database as EntityFramework InMemoryDataBase.
Shared test context. In-memory database. Testing a query that requires authentication -<FakePolicyEvaluator , IPolicyEvaluator>, <FakeUserFilter , IAsyncActionFilter> . Query validation testing. Seed test data. Testing user registration and login. Mocking services. Testing validators and dependency registration.
