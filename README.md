# ShopifyBackendChallenge-S22
This is my solution for the Shopify Backend Intern Developer challenge.

This challenge was completed in Visual Studio using ASP.NET MVC. I made use of this MVC Framework to create a dynamic web page, allowing the user to apply CRUD operations to inventory items.

The second 'task' I chose was to allow a message to be displayed upon deletion, and allow the user to 'un-delete'.
This was done by using a 'soft' delete feature.  Deleted items are NOT removed from database, but rather marked as 'IsDeleted'. 
Deleted items are not displayed in the full list of inventory items.  When an item is deleted, IsDeleted = true.  Clicking UNDO marks it as false.

To run this program, download the program and run using Visual Studio.  Make sure your database is configured, this can be changed in the 'appsettings.json' file. It is currently configured to a database I created.



