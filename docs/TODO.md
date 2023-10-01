# TODO

- Finally got reading from the goods database working; now need to figure out how to create the price tag database.
  - `dotnet ef migrations add InitialCreate --context PriceTagContext` doesn't seem to be working like it should
  - Wait I think this is because I have to actually create the file first... migration doesn't do that I guess?