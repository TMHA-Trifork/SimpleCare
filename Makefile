.PHONY: run build build-container

run:
	docker compose build
	docker compose up

build:
	dotnet build

build-container:
	docker compose build
