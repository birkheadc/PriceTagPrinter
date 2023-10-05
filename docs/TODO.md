# TODO

- Build out the app assuming the Goods database is up-to-date (it's not)
- Figure out how to copy the up-to-date database file from my register
  - Preferably:
    - At-will
    - From within this app
    - Without installing anything on the register

- Need to keep track of when last pull from real database happened.
  - Do I need to do this? I kind of want to just to let the user know, but I don't think it's actually necessary for the app to run.

- Figure out database upload
  - Must be accessible in a no-script environment
  - Endpoint must be available to CURL / automatic uploading
  - Get manual uploading working first
  - Finally, figure out how to schedule the register to do the upload itself automatically, maybe daily