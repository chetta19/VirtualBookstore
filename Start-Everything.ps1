docker build -t books-image -f Backend/Books/Dockerfile .
docker compose -f "docker-compose.yaml" up -d --build