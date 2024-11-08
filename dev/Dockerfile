FROM mcr.microsoft.com/mssql/server:2022-latest

# Switch to root user to perform setup
USER root

# Create a directory for SQL scripts and copy the initialization script
RUN mkdir -p /sqlscripts
COPY init-db.sql /sqlscripts/init-db.sql

# Set up an entrypoint script to initialize the database
COPY entrypoint.sh /usr/src/app/entrypoint.sh
RUN chmod +x /usr/src/app/entrypoint.sh

# Switch back to the non-root user (typically mssql)
USER mssql

EXPOSE 1433
ENTRYPOINT ["/bin/bash", "/usr/src/app/entrypoint.sh"]
