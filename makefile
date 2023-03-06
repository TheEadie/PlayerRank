.PHONY: build
default: build

clean:
	@./build/clean.sh

version:
	@./build/version.sh

build: | version
	@./build/build.sh

test: | build
	@./build/test.sh

publish: | build
	@./build/publish.sh