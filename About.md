# Klipp & Style Salong

## About the project
I created this project to gain a better understanding of how to build an API from scratch using Code First in Entity Framework Core and a Minimal API in .NET. The Booking model was created in code, and I then let Entity Framework generate the database using migrations.

The API is built as a Minimal API with validation for incoming data. I use a DTO to control what information is sent to the client, which gave me a better understanding of how to separate internal models from what is exposed externally.

In the DbContext, I added seed data to quickly test the functionality. To test the API, I integrated Scalar, which made it easy to work in a structured way with my endpoints.
