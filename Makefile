SHELL := /bin/bash

.PHONY: help
.DEFAULT_GOAL := help

# Load environment file if exists
ENV_FILE := .env
ifeq ($(filter $(MAKECMDGOALS),config clean),)
	ifneq ($(strip $(wildcard $(ENV_FILE))),)
		ifneq ($(MAKECMDGOALS),config)
			include $(ENV_FILE)
			export
		endif
	endif
endif

help: ## 💬 This help message :)
	@grep -E '[a-zA-Z_-]+:.*?## .*$$' $(firstword $(MAKEFILE_LIST)) | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-23s\033[0m %s\n", $$1, $$2}'

build: ## 🔨 build the application 
	@echo -e "----\e[34mStart $@\e[0m----" || true
	@cd app && dotnet build 
	@echo -e "----\e[34mCompleted\e[0m----"

run: ## 🏃 Run the application
	@echo -e "----\e[34mStart $@\e[0m----" || true
	@cd app/backend && dotnet run 
	@echo -e "----\e[34mCompleted\e[0m----"

run-watch: ## 🏃 Run the application with hot reload enabled
	@echo -e "----\e[34mStart $@\e[0m----" || true
	@cd app/backend && dotnet watch 
	@echo -e "----\e[34mCompleted\e[0m----"

clean: ## 🏃 clean the application build files
	@echo -e "----\e[34mStart $@\e[0m----" || true
	@cd app && dotnet clean 
	@echo -e "----\e[34mCompleted\e[0m----"

e2e-init:## 🏃 Initialize e2e test environment by installing httpyac CLI
	@echo -e "----\e[34mStart $@\e[0m----" || true
	@npm install -g httpyac
	@echo -e "----\e[34mCompleted\e[0m----"

e2e:## 🏃 Run e2e-test on your local dev environment
	@echo -e "----\e[34mStart $@\e[0m----" || true
	@echo "Please run make run in a separate terminal to start the application"
	@httpyac testing/e2e-test/*/*.http --all -e local
	@echo -e "----\e[34mCompleted\e[0m----"

e2e-rest:## 🏃 Run e2e-test on your local dev environment for the rest integration
	@echo -e "----\e[34mStart $@\e[0m----" || true
	@httpyac testing/e2e-test/rest/*.http --all -e local
	@echo -e "----\e[34mCompleted\e[0m----"

e2e-sdk:## 🏃 Run e2e-test on your local dev environment for the sdk integration
	@echo -e "----\e[34mStart $@\e[0m----" || true
	@echo "Please run make run in a separate terminal to start the application"
	@httpyac testing/e2e-test/sdk/*.http --all -e local
	@echo -e "----\e[34mCompleted\e[0m----"