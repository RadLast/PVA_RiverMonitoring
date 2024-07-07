# RiverMonitoring

RiverMonitoring is a comprehensive platform for real-time river monitoring and alerts.

## Description

This project aims to provide a user-friendly interface for monitoring river conditions, setting alerts for potential flood or drought conditions, and analyzing historical data.

## Features

- **Real-time monitoring**: Get up-to-date information on river conditions.
- **Customizable alerts**: Set thresholds for flood and drought conditions.
- **Interactive map**: View all river stations on a map with detailed information.
- **User authentication and role management**: Secure login system with different access levels.
- **Historical data analysis**: Analyze past data to identify trends and patterns.
- **API key authentication**: Secure API endpoints with token-based authentication.

## Screenshots

![Home Page](path/to/homepage_screenshot.png)

## System Requirements

- **.NET 6 SDK**: The .NET 6 Software Development Kit is required to build and run the application. You can download it from the [official .NET website](https://dotnet.microsoft.com/download/dotnet/6.0).
- **SQL Server**: SQL Server is required for the database. You can use SQL Server Express for development purposes, which can be downloaded from the [official SQL Server website](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
- **Visual Studio 2022**: Visual Studio 2022 is recommended for development. It includes tools for building .NET applications and managing SQL Server databases. You can download it from the [official Visual Studio website](https://visualstudio.microsoft.com/vs/).

## Installation and Setup

1. **Clone the repository**:
    ```sh
    git clone https://github.com/your-username/RiverMonitoring.git
    cd RiverMonitoring
    ```

2. **Configure the database**:
    - Update the connection string in `appsettings.json` to point to your SQL Server instance.

3. **Apply database migrations**:
    ```sh
    dotnet ef database update
    ```

4. **Run the application**:
    ```sh
    dotnet run
    ```

## Usage

1. **Register a new user or log in with an existing account**: Navigate to the registration or login page to access your account.
2. **Navigate to the "Stations" page**: View and manage river stations. You can add, edit, or delete stations as needed.
3. **Use the "History" page**: Analyze historical data and visualize trends in river conditions.
4. **Set up alerts**: Configure alerts for flood and drought conditions for specific river stations.

## Testing

To run tests, use the following command:
```sh
dotnet test
```

## Technologies and Tools

- **ASP.NET Core**: For building the web application.
- **Entity Framework Core**: For database access and management.
- **SQL Server**: As the database.
- **Leaflet.js**: For interactive maps.
- **Bootstrap**: For responsive design and UI components.

## Contributions

Contributions are welcome! Please fork the repository and create a pull request with your changes. Ensure your code follows the project's coding standards and includes appropriate tests.

## Authors

- [Your Name](https://github.com/RadLast)

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.

## Acknowledgements

- Thanks to the ASP.NET Core and Entity Framework Core teams for their excellent documentation and support.
- Special thanks to the contributors of Leaflet.js and Bootstrap for their powerful and easy-to-use libraries.
