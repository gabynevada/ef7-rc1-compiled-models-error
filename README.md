# ef7-rc1-compiled-models-error

## Steps to reproduce

1. Clone this repo
2. Set connection string in `Program.cs`
3. Run
```bash
dotnet restore
dotnet ef database update --project Ef7Rc1Error 
dotnet run --project Ef7Rc1Error  
```
4. Send a post request to `http://localhost:5141/users`


