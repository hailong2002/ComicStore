# ComicStore

# Authentication
    Login with email
        Account:
        - Admin, email: admin@gmail.com , password: 123456
        - Manager, email: manager@gmail.com, password: 123456
        - Customer, email: customer@gmail.com, password: 123456
    Logout
    Register with email 

# Authorization
    Admin: - View users, products, catgories, suppliers.
           - Lock & unlock users of role manager or customer.
    Manager: - Create, update, delete, view products, catgories, suppliers.
    Customer: - View products, catgories, suppliers.

# Product 
    - Create: create new product
    - Edit: edit exist product
    - Delete: delete exist product
    - Read: view product details
    - Search: Search product by name, by category name and by brand.
    - Sort: Sort product by price & name  (increase, decrease) 
    - Filter: Filter product by max price & min price

# Category
    - Create: create new category
    - Edit: edit exist category
    - Delete: delete category without products
    - Read: view category details

# Supplier
    - Create: create new supplier
    - Edit: edit exist supplier
    - Delete: delete supplier without products
    - Read: view supplier details
    - Search: search supplier by name or address.