# HashiCorp Vault ASP.NET Core Demo

This project demonstrates how to securely integrate an ASP.NET Core application with HashiCorp Vault using Kubernetes authentication. Its primary goal is to eliminate the need for storing secrets in `.env` files, source code, configuration files, or user secrets while following production-oriented security practices.

The application retrieves its configuration and secrets directly from Vault during startup using the Kubernetes authentication method, solving the *secret zero* problem by allowing Kubernetes to securely identify the application to Vault without embedding credentials in the application.

## Features

* ASP.NET Core application running in Kubernetes
* HashiCorp Vault deployed in Kubernetes
* Kubernetes authentication for Vault
* Automatic secret retrieval during application startup
* No application secrets stored in:

  * `.env` files
  * `appsettings.json`
  * User Secrets
  * Source code
* TLS-secured communication between the application and Vault
* HTTPS certificate managed through Kubernetes Secrets
* Production-style deployment using dependency injection and configuration binding

## Architecture

```
                 +-----------------------------+
                 |      ASP.NET Core API       |
                 |        (Kubernetes)         |
                 +-------------+---------------+
                               |
                     HTTPS / TLS
                               |
                     Kubernetes Authentication
                               |
                               v
                 +-----------------------------+
                 |      HashiCorp Vault        |
                 |        (Kubernetes)         |
                 +-------------+---------------+
                               |
                         KV Version 2
                               |
                               v
                        Application Secrets
```

## Goals

This project demonstrates modern secret management practices for cloud-native applications by:

* Removing hard-coded secrets from applications
* Eliminating dependency on environment files
* Using Kubernetes as a trusted orchestrator
* Following the principle of least privilege
* Separating secret management from application code
* Providing a foundation for replacing Vault with another secret provider through dependency injection

## Technologies

* ASP.NET Core
* C#
* Kubernetes
* HashiCorp Vault
* Vault Agent Injector
* Kubernetes Authentication
* TLS/HTTPS
* Dependency Injection
* Docker

## Who This Project Is For

This project is intended for developers, architects, and DevOps engineers who want to learn how to build applications that retrieve secrets securely at runtime without embedding credentials in their deployments or source code. It serves as a practical reference for implementing secure secret management in Kubernetes-hosted ASP.NET Core applications.
