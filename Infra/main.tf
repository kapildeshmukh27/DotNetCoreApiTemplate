## 1. Resource Group
#resource "azurerm_resource_group" "rg" {
#  name     = var.resource_group
#  location = var.location
#}

data "azurerm_resource_group" "rg" {
  name     = var.resource_group
}


## 2. App Service Plan (Free F1)
#resource "azurerm_service_plan" "asp" {
#  name                = var.asp_name
#  resource_group_name = azurerm_resource_group.rg.name
#  location            = azurerm_resource_group.rg.location
#  os_type             = "Linux"
#  sku_name            = var.sku
#}

# Data source for existing App Service Plan
data "azurerm_app_service_plan" "asp" {
  name                = "asp-dotnetapi-dev"
  resource_group_name = "rg-dotnetapi-dev"
}

# 3. Web App
resource "azurerm_linux_web_app" "app" {
  name                = var.app_name
  resource_group_name = data.azurerm_resource_group.rg.name
  location            = data.azurerm_app_service_plan.asp.location
  service_plan_id     = data.azurerm_app_service_plan.asp.id

  site_config {
    application_stack {
      dotnet_version = "8.0"
    }
  }

  app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = "1"
    "ApiKey"                   = "super-secure-api-key"
  }
}