#terraform {
#  required_version = ">=1.6.0"
#  backend "azurerm" {
#    resource_group_name  = "rg-terraform-state"
#    storage_account_name = "mystatestorageacct"
#    container_name       = "tfstate"
#    key                  = "app.terraform.tfstate"
#  }
#}

provider "azurerm" {
  features {}

  subscription_id = var.subscription_id
  client_id       = var.client_id
  client_secret   = var.client_secret
  tenant_id       = var.tenant_id
}
