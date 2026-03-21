**Klipp & Style Salon**

_Usage_  
The API can get all the bookings, create new bookings and delete a booking. 

_Project Description_  
A booking backend system for a hair salon. Database can be created through Entity Framework using the project. [More about this project](about.md)

_Purpose_  
This is an exercise in creating Minimal REST-API, validation and async programming. 

_Endpoints_  
GET  
URL: /bookings  
Description: Returns all the bookings in the database by pagination. Query params are page and pageSize, has default page=1 and pageSize=10. Booking data include: Date, Time, Hairdresser, CustomerName, PhoneNr. 

GET  
URL: /bookings/date/{date} 
Description: Returns all the bookings on the requested date (route param). Booking data include: Date, Time, Hairdresser, CustomerName, PhoneNr.

POST  
URL: /bookings  
Description: Receives booking information (JSON) in body to save as booking in the database. Requested data is: Date, Time, Hairdresser, CustomerName, PhoneNr. Validates that customer has given a PhoneNr with digits and their name in booking. Also check that booking time hasn't already passed. 

DELETE  
URL: /bookings/{id}  
Description: Removes a booking from database by booking id. Id is necessary to find the booking and delete it in the system. 
