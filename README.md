# Hangfire Demo

This project showcases different configurations of [Hangfire](https://www.hangfire.io/) in .NET applications. It explores scenarios where the Hangfire client and server are within the same executable and where they are in separate executables.

## Features

- **Combined Client and Server Console Application**: Run both the Hangfire client and server within a single process.
- **Independent Client and Server Applications**:
  - **Client Console Application**: A standalone application acting as a Hangfire client.
  - **Server Console Application**: An independent application running the Hangfire server.
  - **Server ASP.NET Core Application**: An ASP.NET Core application hosting the Hangfire server.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) or [Rancher Desktop](https://rancherdesktop.io/) for container management
- SQL Server (**runs in a Docker container; no local installation required**)

## Getting Started

### Setting Up Infrastructure Dependencies

The project depends on a SQL Server database for Hangfire. To set this up without installing SQL Server locally, use Docker Compose:

1. Navigate to the `dev` directory.
2. Run the following command:

   ```bash
   docker-compose up -d
   ```

This command starts a SQL Server container listening on port `1435`. Ensure you have Docker Desktop or Rancher Desktop installed to run Docker containers.

### Running the Applications

#### Visual Studio Users

If you're using Visual Studio, you can run multiple projects simultaneously using the **Multiple Startup Projects** feature:

1. Right-click on the solution in **Solution Explorer** and select **Properties**.
2. Go to **Startup Project** under **Common Properties**.
3. Choose **Multiple startup projects**.
4. For each project you want to run, set the **Action** to **Start**.
5. Click **OK** to save the settings.

#### Running the Combined Client and Server Console Application

1. Navigate to the `src/Initialization/HangfireDemo.ConsoleApp` directory.
2. Run the application:

   ```bash
   dotnet run
   ```

This starts the console application with both the client and server integrated.

#### Running Independent Client and Server Applications

##### Running the Server Console Application

1. Navigate to `src/Initialization/HangfireServer.ConsoleApp`.
2. Start the server:

   ```bash
   dotnet run
   ```

##### Running the Client Console Application

In a separate terminal:

1. Navigate to `src/Initialization/HangfireClient.ConsoleApp`.
2. Run the client:

   ```bash
   dotnet run
   ```

#### Running the ASP.NET Core Hangfire Server

1. Navigate to `src/Initialization/HangfireServer.WebApp`.
2. Start the web application:

   ```bash
   dotnet run
   ```

Then run the client application as described above to interact with the server.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request if you'd like to contribute to this project.

## License

This project is licensed under the [MIT License](LICENSE).
