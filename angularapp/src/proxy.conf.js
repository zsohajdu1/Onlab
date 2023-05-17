const PROXY_CONFIG = [
  {
    context: [
      "/api",
      "/authentication",
      "/_configuration",
      "/Identity",
      "/.well-known",
      "/weatherforecast",
      "/connect",
      "/ApplyDatabaseMigrations",
      "/_framework",
      "/_vs",
    ],
    target: "https://localhost:7010",
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;