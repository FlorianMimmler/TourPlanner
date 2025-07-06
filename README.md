# Tour Planner

> [!NOTE]  
> This is a university project. Do not enter sensitive data

This app helps you manage tours and tour logs, generate PDF reports, view tours on a map, and more. It supports light and dark themes and allows importing/exporting tour data.

## Features

- **Tour Management:** Create, edit, and delete tours
- **Tour Logs:** Add logs with extra info to each tour
- **PDF Reports:** For a single tour or all tours
- **Map View:** Interactive map using Leaflet
- **Full-Text Search:** Find tours easily
- **Theme Support:** Switch between light and dark mode
- **Import/Export:** Load or save tour data

## Project Structure

- **Presentation Layer:** UI built with MVVM pattern
- **Domain Layer:** Data models
- **Business Layer:** Application logic in services
- **Data Access Layer:** PostgreSQL via EntityFrameworkCore
- **Unit Tests:** Cover ViewModels and Services

## Libraries Used

- `EntityFrameworkCore` + `Npgsql` – Database handling
- `iText7` – For generating PDF reports
- `Leaflet` (via WebView) – For showing maps

## Design Patterns

- **MVVM:** For separating UI and logic
- **Observable Pattern:** Used with `ObservableCollection` for live-updating lists
- **SOLID Principles:** Especially SRP, ISP, and DIP with service interfaces

## Testing

- Unit tests for main ViewModels and services
- PDF tests just check if a file was created
- No database query tests (would need test DB setup)

## Unique Feature

Theme switching: The app can dynamically switch between light and dark mode during runtime using resource dictionaries.
