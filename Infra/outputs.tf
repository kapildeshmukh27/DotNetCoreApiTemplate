output "web_app_url" {
  value = azurerm_linux_web_app.app.default_hostname
}
output "app_service_plan_name" {
  value = azurerm_service_plan.asp.name
}
