# Price Tag Printer

This is the specialized app I use to print price tags at my business.

This repository is made public only to be displayed as a project in my portfolio. The application is designed with my own specific situation in mind and is not meant for general use. This is also my first time writing Blazor.

The application relies on an sqlite file maintained by another device on the same local network. ~~The user can prompt a database copy / query at anytime.~~ The other device is scheduled to upload a copy of the database routinely over http on the local network. The application also keeps track of all items it has printed. Whenever it detects an item has been edited -- the name or price has changed, generally -- it automatically adds that item, with the updated data, to the queue to be reprinted. The user can also manually add items to the queue by scanning or typing the barcode.

The user can view the queue at any time, and print from there.

## Deployment

I don't deploy anything! I just run it in the development server on my local internet! This feels dumb but works well enough for now. One environment variable needs to be set: `ASPNETCORE_BACKENDURL`. I'm sure there's a way to get this variable without setting it explicitly on startup. But at this point I'm tired of dealing with my register, which *doesn't have javascript enabled* and guessing whether something will work on it or not. So I just launch the server like so:

`ASPNETCORE_BACKENDURL=http://my.local.ip.address:port dotnet run`

## Repo-Parser
This repository is parse-able by my custom repo-parser. The contents of the `repo-parser-target` directory are meant to be consumed by an API using this package.
### GithubRepoParser
- source code: [https://github.com/birkheadc/github-repo-parser]
- npm package: (not yet published)
