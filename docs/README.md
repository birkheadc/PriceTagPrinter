# Price Tag Printer

This is the specialized app I use to print price tags at my business.

This repository is made public only to be displayed as a project in my portfolio. The application is designed with my own specific situation in mind and is not meant for general use. This is also my first time writing Blazor.

The application relies on an sqlite file maintained by another device on the same local network. The user can prompt a database copy / query at anytime. This connects to the other device and copies the relevant data. The application also keeps track of all items it has printed. Whenever it detects an item has been edited -- the name or price has changed, generally -- it automatically adds that item, with the updated data, to the queue to be reprinted. The user can also manually add items to the queue by scanning or typing the barcode.

The user can view the queue at any time, and print from there.