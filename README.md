# Run

## Prerequisites

- Docker installed
- This repository cloned locally

Execute the following command from the root of the repository to build the docker image

`docker build -f ./TrueLayer.Api/Dockerfile -t charliekendall/truelayer .`

Spin up a docker container from the image

`docker run -d -p 5002:80 --name truelayer charliekendall/truelayer`

Then if you have Postman or Curl, you can use that to make requests to the API

`GET http://localhost:5002/api/v1.0/pokemon/translated/charmander`

Alternatively, you can pull and use a docker image that has curl installed, for example:

`docker run --rm byrnedo/alpine-curl http://host.docker.internal:5002/api/v1.0/pokemon/translated/charmander -v`

# Remaining tasks for production

- Add liveness and readiness healthcheck endpoints
- Add some resiliency policies to the HttpClients (e.g. retry with exponential backoff, fallback, circuit-breaker)
- Add more integration tests to verify that the third party APIs don't change
- Add more unit test coverage
- Add metrics reporting (CPU, memory, concurrent requests, requests to third party APIs, etc.)
- Add a more observable log sink (e.g. Elasticsearch)
- Assuming data doesn't change, add response caching
- Add Dockerfiles for test projects