# Social Media App

A full-featured social media platform built with ASP.NET Core 8.0 MVC, featuring community-based discussions, posts, and threaded comments.

## Features

### Core Functionality
- **User Authentication & Authorization**
  - ASP.NET Core Identity integration
  - Secure user registration and login
  - Profile management
  - Soft delete for user accounts

- **Communities**
  - Create and manage topic-based communities
  - Join/leave communities
  - Community-specific posts
  - Member management
  - Soft delete support

- **Posts**
  - Create, edit, and delete posts within communities
  - Rich text content (up to 2000 characters)
  - Post title validation (5-30 characters)
  - User ownership verification
  - Soft delete functionality

- **Comments**
  - Threaded comment system
  - Reply to comments (nested structure)
  - Edit and delete comments
  - AJAX-powered comment loading
  - Show/hide replies functionality
  - Comment ownership verification

### Technical Features
- **Soft Delete Pattern**
  - All major entities support soft deletion
  - Data retention for audit purposes
  - Custom interceptor implementation

- **Repository Pattern**
  - Clean separation of concerns
  - Unit of Work pattern for transaction management
  - Service layer abstraction

- **Responsive Design**
  - Bootstrap 5 integration
  - Mobile-friendly interface
  - Bootstrap Icons
  - Custom CSS styling

- **Pagination**
  - X.PagedList integration for efficient data browsing
  - Paginated community lists
  - Paginated post lists

## Architecture

### Project Structure
Social_Media_App/
├── Core/                       # Domain models and interfaces
│   ├── Interfaces/
│   │   ├── Repository/        # Repository interfaces
│   │   ├── Services/          # Service interfaces
│   │   └── ISoftDeleteable.cs
│   ├── Models/
│   │   ├── ApplicationUser.cs
│   │   ├── Community.cs
│   │   ├── Post.cs
│   │   ├── Comment.cs
│   │   └── Membership.cs
│   ├── ViewModel/             # Data transfer objects
│   └── Attributes/            # Custom validation attributes
│
├── Data/                      # Data access layer
│   ├── AppDbContext.cs
│   ├── Interceptors/
│   │   └── SoftDeleteInterceptor.cs
│   ├── Repositories/          # Repository implementations
│   └── Configurations/        # EF Core entity configurations
│
├── Services/                  # Business logic layer
│   ├── CommentService.cs
│   ├── PostService.cs
│   ├── CommunityService.cs
│   ├── UserService.cs
│   ├── MembershipService.cs
│   └── HomeService.cs
│
└── Web/                       # Presentation layer (MVC)
    ├── Controllers/
    ├── Views/
    ├── wwwroot/               # Static files
    └── Program.cs
### Technology Stack

#### Backend
- **Framework**: ASP.NET Core 8.0 MVC
- **ORM**: Entity Framework Core 8.0.19
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity
- **Architecture**: Repository Pattern + Unit of Work + Service Layer

#### Frontend
- **UI Framework**: Bootstrap 5
- **Icons**: Bootstrap Icons 1.10.5
- **JavaScript**: jQuery
- **Validation**: jQuery Validation + Unobtrusive Validation

#### Key NuGet Packages
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (8.0.19)
- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.19)
- `Microsoft.EntityFrameworkCore.Tools` (8.0.19)
- `X.PagedList.Mvc.Core` (10.5.9)
- `Microsoft.VisualStudio.Web.CodeGeneration.Design` (8.0.7)



## Database Schema

### Core Entities

**ApplicationUser**
- Extends IdentityUser
- Supports soft delete
- Related to Communities, Posts, Comments

**Community**
- Name (5-15 characters, unique)
- Description (max 255 characters)
- Many-to-many relationship with Users via Membership
- One-to-many with Posts

**Post**
- Title (5-30 characters)
- Content (max 2000 characters)
- Belongs to Community and User
- Has many Comments

**Comment**
- Content (max 300 characters)
- Self-referential relationship for threading
- Belongs to Post and User
- Supports nested replies

**Membership**
- Join table for User-Community relationship
- Tracks join date

## Key Features Implementation

### Soft Delete Pattern

All major entities implement `ISoftDeleteable`:
public interface ISoftDeleteable
{
    bool IsDeleted { get; set; }
    DateTime? DeleteDate { get; set; }
    void Delete();
}
The `SoftDeleteInterceptor` automatically marks entities as deleted instead of removing them from the database.

### Threaded Comments

Comments support infinite nesting through a self-referential relationship:
- AJAX-powered "Show Replies" functionality
- Efficient data loading using a combination of Eager Loading (Include) for initial display and AJAX for on-demand replies
- Visual indentation for comment hierarchy

### Repository Pattern

Generic repository with specialized implementations:
- `IPostRepository`
- `ICommentRepository`
- `ICommunityRepository`
- `IMembershipRepository`

### Service Layer

Business logic separated into dedicated services:
- `ICommentService` - Comment operations
- `IPostService` - Post management
- `ICommunityService` - Community operations
- `IUserService` - User management
- `IMembershipService` - Membership operations
- `IHomeService` - Home page logic






### Main Controllers

- **HomeController** - Landing page and general navigation
- **AccountController** - User registration, login, profile
- **CommunityController** - Community CRUD operations
- **PostController** - Post management
- **CommentController** - Comment operations


## 🚀 Getting Started

Follow these instructions to get a copy of the project up and running on your local machine.



### Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/ahmed151515/Social_Media_App.git
    cd Social_Media_App

    ```

2.  **Configure the database connection:**
    -   Open the solution in your preferred code editor (like Visual Studio or VS Code).
    -   In the `Web` project, open `appsettings.json`.
    -   Update the `dev` connection string to point to your local SQL Server instance.
    ```json
    "ConnectionStrings": {
      "dev": [Your Connection Strings]
    }
    ```

3.  **Apply database migrations:**
    -   Open a terminal in the root directory of the solution.
    -   Run the following command
    ```bash
    dotnet ef database update --project Data --startup-project Web
    ```
    

4.  **Run the application:**
    -   Navigate to the `Web` project folder in your terminal and run:
    ```bash
    dotnet run --project web
    ```
    -   Or simply press `F5` in Visual Studio.