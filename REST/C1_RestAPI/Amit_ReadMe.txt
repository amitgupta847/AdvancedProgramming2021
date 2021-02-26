1. AppStore - this is a web API exposing all the endpoints.
Note - Currently app is using Header based versioning, so make sure you pass the header
X-API-Version with value 1.0 or 2.0 based on the controller you would like to access.


2. StoreWebClient - this one is acting as an client to Web APIs.


3. Store.IdentityServer - 
As name suggest this one is to generate token or kind of responsible for validating users.
We created an inmemory client details in this project.


4. Store.SecureConsoleClient - 
Console based project, which act as client and provide the login details to talk to Identity server,
and inturn identity server gives the token to it.
We can copy this token and use in postman to access the WebApi using Bearer as auth.

-- Feb 23 2021