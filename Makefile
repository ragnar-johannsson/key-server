NAME=ragnarb/key-server
VERSION=$(shell git describe --long --tags master | cut -d - -f 1-1)

default: build

deps:
	dotnet restore

build: deps
	dotnet build

.PHONY: container
container:
	docker build --tag ${NAME} ${CURDIR}
	docker tag ${NAME}:latest ${NAME}:${VERSION}

.PHONY: clean
clean:
	rm -rf obj/ bin/
	-docker rmi --force ${NAME}:latest
	-docker rmi --force ${NAME}:${VERSION}

