provider "kafka" {
  bootstrap_servers = ["localhost:9092"]
  tls_enabled = false  
}

resource "kafka_topic" "hotel-created" {
  name               = "hotel-created"
  replication_factor = 1
  partitions         = 1

  config = {
    "segment.ms"     = "20000"
    "cleanup.policy" = "compact"
  }
}

resource "kafka_topic" "room-added" {
  name               = "room-added"
  replication_factor = 1
  partitions         = 1

  config = {
    "segment.ms"     = "20000"
    "cleanup.policy" = "compact"
  }
}

resource "kafka_topic" "hotel-address-updated" {
  name               = "hotel-address-updated"
  replication_factor = 1
  partitions         = 1

  config = {
    "segment.ms"     = "20000"
    "cleanup.policy" = "compact"
  }
}

resource "kafka_topic" "hotel-contacts-updated" {
  name               = "hotel-contacts-updated"
  replication_factor = 1
  partitions         = 1

  config = {
    "segment.ms"     = "20000"
    "cleanup.policy" = "compact"
  }
}