.PHONY: run build build-container

run:
	docker compose build
	docker compose up --wait --detach
	cd "SimpleCare.Infrastructure/" && \
	dotnet ef database update && \
	cd ..

build:
	dotnet build

build-container:
	docker compose build

stop:
	docker compose down
