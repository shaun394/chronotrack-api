# ChronoTrack API

ChronoTrack is a Clockify-inspired time tracking API built with ASP.NET Core, Clean Architecture, CQRS, DDD, and PostgreSQL.

The goal of this project is to build a backend-first time tracking system that can later support a React and TypeScript frontend.

## Planned Features

- Workspace management
- User and member management
- Client management
- Project management
- Project tasks
- Tags
- Manual time entries
- Start/stop timer tracking
- Summary and detailed reports
- Time approval workflow
- Audit trail for important changes

## Architecture

The solution follows a four-layer Clean Architecture structure:

```text
ChronoTrack.Api
ChronoTrack.Application
ChronoTrack.Domain
ChronoTrack.Infrastructure