# Home Services Platform

A .NET Core MVC website designed to connect customers with skilled service providers, streamlining the process of finding and booking a variety of services. Customers can explore service provider profiles, filter based on preferences, and schedule services according to real-time availability. Service providers can showcase their expertise, manage bookings, and receive feedback, creating a smooth experience for both parties.

## Demo

[Watch the Demo](https://youtu.be/umjjbh3n3DA?si=my9EFcx5-CyMXvsB)  <!-- Link to YouTube video demo here -->

---

## Table of Contents

- [Project Overview](#project-overview)
- [Project Features](#project-features)
- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
- [Future Enhancements](#future-enhancements)
- [Contact](#contact)

---

## Project Overview

This Home Services Platform is designed to bridge the gap between customers seeking various services and providers offering those services. It enables customers to browse available providers, view their profiles, and schedule bookings based on the providers' real-time availability. Service providers, in turn, can manage their profiles, update availability, and interact with customer requests.

---

## Project Features

### 1. Registration & Profile Setup

**Customers**:
- Sign up using email/password.
- Create a profile with basic details (name, contact info, address, and booking history).

**Service Providers**:
- Register using email/password.
- Set up a comprehensive profile, including a bio, contact information, address, portfolio, availability calendar, pricing rates, reviews, and ratings.

---

### 2. Browsing and Searching Services

**Customers**:
- Select a specific category of services.
- View profiles of all service providers within the chosen category.
- Filter results by:
    - Type of service
    - City
    - Ratings

- Profile previews in search results display:
    - Provider name
    - Address
    - Bio
    - Profile picture
    - Price range
    - Rating

---

### 3. Service Selection & Details

**Customers**:
- Access detailed service provider profiles, including:
    - Bio, services offered, and price range.
    - Availability calendar.
    - Portfolio with images and captions.
    - Customer ratings and reviews.

---

### 4. Booking & Scheduling

**Customers**:
- Submit a request to a provider.
- Enter booking details:
    - Preferred communication method (e.g., WhatsApp, Call)
    - Additional comments

- Bookings are added to the customer's booking history, with options to filter by service and status (on review, complete, accepted, rejected).

**Service Providers**:
- View booking requests in a dedicated request history page.
- Approve or reject requests.
- Filter requests by status (on review, complete, accepted).

---

### 5. Job Completion & Feedback

**Customers**:
- Post-service, customers can rate and review providers (1-5 stars and a written review).

**Service Providers**:
- Mark requests as completed after the service is delivered.

---

## Technologies Used

- **SQL Server**: Database management
- **C#**: Backend programming
- **LINQ & Entity Framework**: Data handling
- **ASP.NET Core MVC**: Framework for building dynamic web applications

---

## Setup Instructions

1. Clone the repository:  
   ```bash
   git clone https://github.com/your-repo/home-services-platform.git
   
2. Navigate to the project folder:
   ```bash
   cd home-services-platform
   
3. Configure the database connection string in appsettings.json with your SQL Server details.
   
4. Run migrations to set up the database schema.
   
5. dotnet ef database update:
   ```bash
    dotnet ef database update
 
6. Build the project:
   ```bash
   dotnet build

7. Start the application:
    ```bash
   dotnet run
    
  ---








   
