#!/bin/bash

#Debugging: Print environment variables to verify they are set correctly
echo "Environment variables:"
echo "ACCEPT_EULA: $ACCEPT_EULA"
echo "SA_PASSWORD: $MSSQL_SA_PASSWORD"

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start (use a loop to check readiness)
echo "Waiting for SQL Server to start..."
until /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -Q "SELECT 1" &> /dev/null
do
  echo -n "."
  sleep 1
done
echo -e "\nSQL Server is ready!"

# Run the initialization script
echo "Running initialization script..."
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -i /sqlscripts/init-db.sql
echo "Database initialization completed."

# Wait indefinitely to keep the container running
wait
