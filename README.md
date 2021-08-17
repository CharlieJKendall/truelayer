# Remaining tasks for production

- Add liveness and readiness healthcheck endpoints
- Add some resiliency policies to the HttpClients (e.g. retry with exponential backoff, fallback, circuit-breaker)
- Add more integration tests to verify that the third party APIs don't change
- Add more unit test coverage
- Add metrics reporting (CPU, memory, concurrent requests, requests to third party APIs, etc.)
- Add a more observable log sink (e.g. Elasticsearch)
- Assuming data doesn't change, add response caching