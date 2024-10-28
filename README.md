# Blazor Bitcoin Price Tracker

This project is a **Blazor Server application** that displays live Bitcoin prices, converting the prices to EUR and CZK using exchange rates fetched from the Czech National Bank (CNB) API. The app offers real-time price updates, stores selected data, and provides UI controls for managing the saved records.

## Features

- **Live Bitcoin Price Updates**: Displays current Bitcoin prices in EUR and CZK.
- **Periodic Data Refresh**: Updates Bitcoin prices at a set interval (configurable in app settings).
- **Save Data to Database**: Allows users to save current price records for future reference.
- **Manage Saved Data**: View saved records, add notes, and delete selected records.

## Prerequisites

- **VS 2022**
- **.NET SDK 8.0 or higher**
- **MSSQL Server** (for saving Bitcoin price records with notes)

## Project Setup

1. Update `appsettings.json`: Change `ConnectionString` to your database if you are not using local db or if you need to change the db name.
2. Open `Package Manager Console` and run `Update-Database` which will create necessary database and tables.
3. Run the project in VS

## Room for improvement

There are at least a few things that could be done better:

1. Tests - Unit tests could be useful in bigger projects
2. Caching - eur to czk rates don't have to be downloaded every 30 seconds.
3. Call APIs in parallel - could save some time.

