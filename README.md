# GAMigrationTool
Tool for retrieveing the secrets from the Google Authenticator app to transfer accounts to for example Authy.

![Example](https://media.discordapp.net/attachments/249596909877067776/897558103036424212/unknown.png)

## Windows
```
Usage: GAMigrationTool.exe <otpData>

Instructions:
1. Scan the export accounts QR Code from the Google Authenticator app
2. Give the whole string as in input to this application.
3. Check the generated accounts.txt or console for results.

Example:
GAMigrationTool.exe otpauth-migration://offline/?data=CjAKCrlQshUNlIgoknISEnhmaWxlRklOJ3MgQWNjb3VudBoIeGZpbGVGSU4gASgBMAIQARgBKNXJ3tcC
```

## Linux
```
Usage: dotnet GAMigrationTool.dll <otpData>

Instructions:
1. Scan the export accounts QR Code from the Google Authenticator app
2. Give the whole string as in input to this application.
3. Check the generated accounts.txt or console for results.

Example:
dotnet GAMigrationTool.dll otpauth-migration://offline/?data=CjAKCrlQshUNlIgoknISEnhmaWxlRklOJ3MgQWNjb3VudBoIeGZpbGVGSU4gASgBMAIQARgBKNXJ3tcC
```