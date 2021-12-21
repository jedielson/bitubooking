# Kafka Tests

## How to use avrogen

Sample for generating avrogen

``` bash
dotnet avrogen -s src/BituBooking.Infra.Kafka/Contracts/hotel-created.avsc src/BituBooking.Infra.Kafka/Contracts/Events
```

## Using Cake Build

### First Things First

``` bash
docker-compose up -d
```

You can access Kafka cluster in `http://localhost:3030`.

### Setup Kafka Topics

``` bash
dotnet cake --target terraform-apply
```

### Databases operations

### Add Migration

``` bash
dotnet cake --migration=First --target migration-add
```

#### Update database

``` bash
dotnet cake --target database-update
```

#### Clean the database

``` bash
dotnet cake --target database-clean
```

### How to test integrations

``` bash
dotnet cake --target dotnet-test 
```