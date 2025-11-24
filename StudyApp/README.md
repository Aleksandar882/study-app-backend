# StudyApp Backend API

This is the backend API for **StudyApp**, a web application for learning programming. It provides endpoints to manage courses, lessons, and track user progress.

## Features

- CRUD operations for **Courses** and **Lessons**.
- Track **lesson completion** for users.
- User authentication with **JWT tokens**.
- Role-based access (admin users can create, edit, delete courses and lessons).

## Tech Stack

- **.NET 7 / ASP.NET Core** (backend framework)
- **Entity Framework Core** (ORM for database access)
- **PostgreSQL** (database)
- **JWT Authentication** (user auth)
- **C#** (programming language)

## Endpoints

- **Courses**
  - `GET /courses` – list all courses
  - `GET /courses/{id}` – get course details with lessons
  - `POST /courses` – create a course (admin only)
  - `PUT /courses/{id}` – update a course (admin only)
  - `DELETE /courses/{id}` – delete a course (admin only)

- **Lessons**
  - `GET /lessons/{id}` – get lesson details
  - `POST /lessons` – create a lesson (admin only)
  - `PUT /lessons/{id}` – update a lesson (admin only)
  - `DELETE /lessons/{id}` – delete a lesson (admin only)

- **User Progress**
  - `GET /userprogress/me` – get logged-in user's progress
  - `POST /userprogress/complete/{lessonId}` – mark lesson as completed