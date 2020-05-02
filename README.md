IRi Product Filter
=======================================

Setup
1) Ensure that the .NET Core 3.0 SDK is installed and setup for compilation through Visual Studio 2019

# Features:
* Returns a distinct list of products and their code types with their latest codes
* Retailer products page
* IRi Products page

# Design Choices and Architecture:

Movies solution has been developed in .NET Core 3.1 using the existing boilerplate with many modifications. It has been developed using the Onion architecture consisting of the following in order from the inner most layer to the outer most layer:

* IRi.Core
* IRi.Data
* IRi.Business
* IRi.Web

This allows more moduler and cleaner code to be written and it also avoids any issues in Dependency Injection such as a circular dependency from when two services attempt to access each other. Furthermore this solution also uses SOLID principles:

# SOLID Implementation:
* S - Single-responsiblity principle in the form of Individual services for a responsibility such as RetailerProductService which is then injected into HomeController
* O - Open-closed principle: static extensions such as IEnumerableExtensions
* L - Liskov substitution principle: inheritance of CsvSettings for Retailer in IRi.Business
* I - Interface segregation principle: use of Interfaces for services
* D - Dependency Inversion Principle: Use of injected services into controllers and services


# Testing:
Unit tests are structured in their own folder as according to class libaries. Unit tests have been built for IProductUtility and its implementation ProductUtility to test the core functionality of the site, which ti filter products by their product type code and then return the latest, this unit of work was tested using:
* Arrange
* Act
* Assert