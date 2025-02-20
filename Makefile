.PHONY: run build

run:
	docker compose up

build:
	dotnet build
