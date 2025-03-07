.PHONY: run build build-container

run:
	docker compose build
	docker compose up
	push ./SimpleCare.Infrastructure/
	dotnet ef database update
	popd

build:
	dotnet build

build-container:
	docker compose build
