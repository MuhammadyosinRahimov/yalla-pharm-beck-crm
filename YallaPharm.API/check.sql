CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE application_methods (
    id character varying(36) NOT NULL,
    application_method character varying(100) NOT NULL,
    CONSTRAINT "PK_application_methods" PRIMARY KEY (id)
);

CREATE TABLE categories (
    id character varying(36) NOT NULL,
    name character varying(100) NOT NULL,
    parent_id character varying(36) NULL,
    CONSTRAINT "PK_categories" PRIMARY KEY (id),
    CONSTRAINT "FK_categories_categories_parent_id" FOREIGN KEY (parent_id) REFERENCES categories (id) ON DELETE RESTRICT
);

CREATE TABLE clients (
    id character varying(36) NOT NULL,
    full_name character varying(30) NOT NULL,
    street character varying(500) NULL,
    landmark character varying(500) NULL,
    geolocation_of_client_address character varying(255) NULL,
    phone_number character varying(20) NOT NULL,
    age smallint NULL,
    social_username character varying(100) NULL,
    language character varying(10) NULL,
    having_children boolean NOT NULL DEFAULT FALSE,
    having_elderly boolean NOT NULL DEFAULT FALSE,
    gender character varying(20) NULL,
    family_status character varying(30) NULL,
    economic_standing character varying(30) NULL,
    childrens_age character varying(30) NULL,
    type character varying(30) NULL,
    contact_type character varying(30) NULL,
    take_into_account character varying(255) NULL,
    discount_for_client integer NOT NULL DEFAULT 0,
    CONSTRAINT "PK_clients" PRIMARY KEY (id)
);

CREATE TABLE for_whom (
    id character varying(36) NOT NULL,
    for_whom character varying(100) NOT NULL,
    CONSTRAINT "PK_for_whom" PRIMARY KEY (id)
);

CREATE TABLE organs_and_systems (
    id character varying(36) NOT NULL,
    organs_and_system character varying(100) NOT NULL,
    CONSTRAINT "PK_organs_and_systems" PRIMARY KEY (id)
);

CREATE TABLE packaging_materials (
    id character varying(36) NOT NULL,
    packaging_material character varying(100) NOT NULL,
    CONSTRAINT "PK_packaging_materials" PRIMARY KEY (id)
);

CREATE TABLE packaging_types (
    id character varying(36) NOT NULL,
    name character varying(100) NOT NULL,
    CONSTRAINT "PK_packaging_types" PRIMARY KEY (id)
);

CREATE TABLE packaging_units (
    id character varying(36) NOT NULL,
    name character varying(100) NOT NULL,
    CONSTRAINT "PK_packaging_units" PRIMARY KEY (id)
);

CREATE TABLE payment_methods (
    id character varying(36) NOT NULL,
    name character varying(100) NOT NULL,
    CONSTRAINT "PK_payment_methods" PRIMARY KEY (id)
);

CREATE TABLE pharmacies (
    id character varying(36) NOT NULL,
    name character varying(100) NULL,
    address character varying(500) NULL,
    landmark character varying(500) NULL,
    contact character varying(100) NULL,
    geolocation_link character varying(500) NULL,
    country character varying(100) NULL,
    markup numeric(18,2) NULL,
    markup_type character varying(30) NULL,
    is_required boolean NOT NULL DEFAULT FALSE,
    is_abroad boolean NOT NULL DEFAULT FALSE,
    payout_method character varying(50) NULL,
    CONSTRAINT "PK_pharmacies" PRIMARY KEY (id)
);

CREATE TABLE preparation_colors (
    id character varying(36) NOT NULL,
    preparation_color character varying(100) NOT NULL,
    CONSTRAINT "PK_preparation_colors" PRIMARY KEY (id)
);

CREATE TABLE preparation_materials (
    id character varying(36) NOT NULL,
    preparation_material character varying(100) NOT NULL,
    CONSTRAINT "PK_preparation_materials" PRIMARY KEY (id)
);

CREATE TABLE products (
    id character varying(36) NOT NULL,
    name character varying(50) NOT NULL,
    type character varying(50) NOT NULL,
    dosage character varying(5000) NOT NULL,
    count_on_package integer NULL,
    age_from integer NULL,
    age_to integer NULL,
    price_with_markup numeric(18,2) NOT NULL,
    price_without_markup numeric(18,2) NULL,
    manufacturer character varying(50) NOT NULL,
    path_image character varying(500) NOT NULL,
    dificit boolean NOT NULL DEFAULT FALSE,
    release_form character varying(50) NOT NULL,
    packaging_unit character varying(50) NOT NULL,
    type_of_packaging character varying(50) NOT NULL,
    link_product character varying(5000) NOT NULL,
    country character varying(500) NOT NULL,
    age_type character varying(20) NULL,
    is_required boolean NOT NULL DEFAULT FALSE,
    state character varying(20) NULL,
    CONSTRAINT "PK_products" PRIMARY KEY (id)
);

CREATE TABLE providers (
    id character varying(36) NOT NULL,
    name character varying(100) NOT NULL,
    contact_type character varying(30) NULL,
    contact character varying(100) NOT NULL,
    country character varying(100) NOT NULL,
    city character varying(100) NOT NULL,
    link_from_where_found_abroad character varying(500) NOT NULL,
    CONSTRAINT "PK_providers" PRIMARY KEY (id)
);

CREATE TABLE release_forms (
    id character varying(36) NOT NULL,
    name character varying(100) NOT NULL,
    CONSTRAINT "PK_release_forms" PRIMARY KEY (id)
);

CREATE TABLE scope_of_applications (
    id character varying(36) NOT NULL,
    scope_of_application character varying(100) NOT NULL,
    CONSTRAINT "PK_scope_of_applications" PRIMARY KEY (id)
);

CREATE TABLE time_of_applications (
    id character varying(36) NOT NULL,
    time_of_application character varying(100) NOT NULL,
    CONSTRAINT "PK_time_of_applications" PRIMARY KEY (id)
);

CREATE TABLE users (
    id character varying(36) NOT NULL,
    first_name character varying(30) NOT NULL,
    last_name character varying(30) NOT NULL,
    phone_number character varying(20) NOT NULL,
    email character varying(100) NOT NULL,
    password character varying(200) NOT NULL,
    role character varying(20) NOT NULL,
    CONSTRAINT "PK_users" PRIMARY KEY (id)
);

CREATE TABLE addresses (
    id character varying(36) NOT NULL,
    client_id character varying(36) NOT NULL,
    street character varying(500) NOT NULL,
    landmark character varying(500) NOT NULL,
    city character varying(100) NOT NULL,
    geolocation_of_client_address character varying(500) NULL,
    CONSTRAINT "PK_addresses" PRIMARY KEY (id),
    CONSTRAINT "FK_addresses_clients_client_id" FOREIGN KEY (client_id) REFERENCES clients (id) ON DELETE CASCADE
);

CREATE TABLE client_adults (
    id character varying(36) NOT NULL,
    client_id character varying(36) NOT NULL,
    full_name character varying(100) NOT NULL,
    age smallint NULL,
    gender character varying(20) NULL,
    CONSTRAINT "PK_client_adults" PRIMARY KEY (id),
    CONSTRAINT "FK_client_adults_clients_client_id" FOREIGN KEY (client_id) REFERENCES clients (id) ON DELETE CASCADE
);

CREATE TABLE client_childrens (
    id character varying(36) NOT NULL,
    client_id character varying(36) NOT NULL,
    full_name character varying(100) NOT NULL,
    age smallint NULL,
    gender character varying(20) NULL,
    CONSTRAINT "PK_client_childrens" PRIMARY KEY (id),
    CONSTRAINT "FK_client_childrens_clients_client_id" FOREIGN KEY (client_id) REFERENCES clients (id) ON DELETE CASCADE
);

CREATE TABLE orders (
    id character varying(36) NOT NULL,
    client_id character varying(36) NOT NULL,
    order_number character varying(50) NOT NULL,
    comment character varying(5000) NULL,
    country_to_delivery character varying(100) NULL,
    courier character varying(100) NULL,
    comment_for_courier character varying(500) NULL,
    operator character varying(100) NOT NULL,
    total_cost numeric(18,2) NOT NULL,
    prepayment numeric(18,2) NULL,
    rest_payment numeric(18,2) NULL,
    discount numeric(18,2) NULL,
    total_order_amount_excluding_delivery numeric(18,2) NULL,
    city_or_district character varying(100) NOT NULL,
    price_for_delivery_outside_the_city numeric(18,2) NULL,
    remaining_payment numeric(18,2) NULL,
    amount_with_discount numeric(18,2) NULL,
    amount_with_markup numeric(18,2) NULL,
    amount_without_markup numeric(18,2) NULL,
    amount_with_delivery numeric(18,2) NULL,
    amount_without_delivery numeric(18,2) NULL,
    type character varying(30) NULL,
    comes_from character varying(30) NULL,
    delivery_type character varying(30) NULL,
    delivered_at character varying(50) NULL,
    CONSTRAINT "PK_orders" PRIMARY KEY (id),
    CONSTRAINT "FK_orders_clients_client_id" FOREIGN KEY (client_id) REFERENCES clients (id) ON DELETE CASCADE
);

CREATE TABLE pharmacy_contacts (
    id character varying(36) NOT NULL,
    pharmacy_id character varying(36) NOT NULL,
    contact character varying(100) NOT NULL,
    contact_type character varying(30) NULL,
    CONSTRAINT "PK_pharmacy_contacts" PRIMARY KEY (id),
    CONSTRAINT "FK_pharmacy_contacts_pharmacies_pharmacy_id" FOREIGN KEY (pharmacy_id) REFERENCES pharmacies (id) ON DELETE CASCADE
);

CREATE TABLE product_providers (
    id character varying(36) NOT NULL,
    product_id character varying(36) NOT NULL,
    provider_id character varying(36) NOT NULL,
    CONSTRAINT "PK_product_providers" PRIMARY KEY (id),
    CONSTRAINT "FK_product_providers_products_product_id" FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE,
    CONSTRAINT "FK_product_providers_providers_provider_id" FOREIGN KEY (provider_id) REFERENCES providers (id) ON DELETE CASCADE
);

CREATE TABLE product_templates (
    id character varying(36) NOT NULL,
    category_id character varying(36) NOT NULL,
    name character varying(100) NOT NULL,
    nickname character varying(100) NULL,
    active_ingredient character varying(100) NULL,
    dosage character varying(500) NULL,
    dosage_unit character varying(30) NULL,
    minimum_quantity_per_piece integer NULL,
    pack_quantity_or_drug_volume character varying(500) NOT NULL,
    packaging_unit character varying(30) NULL,
    preparation_taste character varying(50) NULL,
    age_from smallint NULL,
    age_to smallint NULL,
    instructions character varying(1000) NULL,
    indication_for_use character varying(1000) NOT NULL,
    contraindication_for_use character varying(1000) NOT NULL,
    symptom character varying(1000) NULL,
    for_allergy_sufferers character varying(30) NULL,
    for_diabetics character varying(30) NULL,
    for_pregnant_women character varying(30) NULL,
    for_children character varying(30) NULL,
    for_driver character varying(30) NULL,
    season_of_application character varying(30) NULL,
    dried_fruit character varying(100) NULL,
    comment character varying(5000) NULL,
    with_caution character varying(1000) NULL,
    vacation_condition character varying(30) NULL,
    for_whom_id character varying(36) NOT NULL,
    application_method_id character varying(36) NOT NULL,
    organs_and_systems_id character varying(36) NULL,
    packaging_material_id character varying(36) NOT NULL,
    preparation_color_id character varying(36) NOT NULL,
    preparation_material_id character varying(36) NOT NULL,
    scope_of_application_id character varying(36) NULL,
    time_of_application_id character varying(36) NOT NULL,
    CONSTRAINT "PK_product_templates" PRIMARY KEY (id),
    CONSTRAINT "FK_product_templates_application_methods_application_method_id" FOREIGN KEY (application_method_id) REFERENCES application_methods (id) ON DELETE CASCADE,
    CONSTRAINT "FK_product_templates_categories_category_id" FOREIGN KEY (category_id) REFERENCES categories (id) ON DELETE CASCADE,
    CONSTRAINT "FK_product_templates_for_whom_for_whom_id" FOREIGN KEY (for_whom_id) REFERENCES for_whom (id) ON DELETE CASCADE,
    CONSTRAINT "FK_product_templates_organs_and_systems_organs_and_systems_id" FOREIGN KEY (organs_and_systems_id) REFERENCES organs_and_systems (id) ON DELETE SET NULL,
    CONSTRAINT "FK_product_templates_packaging_materials_packaging_material_id" FOREIGN KEY (packaging_material_id) REFERENCES packaging_materials (id) ON DELETE CASCADE,
    CONSTRAINT "FK_product_templates_preparation_colors_preparation_color_id" FOREIGN KEY (preparation_color_id) REFERENCES preparation_colors (id) ON DELETE CASCADE,
    CONSTRAINT "FK_product_templates_preparation_materials_preparation_materia~" FOREIGN KEY (preparation_material_id) REFERENCES preparation_materials (id) ON DELETE CASCADE,
    CONSTRAINT "FK_product_templates_scope_of_applications_scope_of_applicatio~" FOREIGN KEY (scope_of_application_id) REFERENCES scope_of_applications (id) ON DELETE SET NULL,
    CONSTRAINT "FK_product_templates_time_of_applications_time_of_application_~" FOREIGN KEY (time_of_application_id) REFERENCES time_of_applications (id) ON DELETE CASCADE
);

CREATE TABLE countries (
    id character varying(36) NOT NULL,
    country character varying(100) NOT NULL,
    city character varying(100) NOT NULL,
    order_id character varying(36) NOT NULL,
    CONSTRAINT "PK_countries" PRIMARY KEY (id),
    CONSTRAINT "FK_countries_orders_order_id" FOREIGN KEY (order_id) REFERENCES orders (id) ON DELETE CASCADE
);

CREATE TABLE order_histories (
    id character varying(36) NOT NULL,
    order_id character varying(36) NOT NULL,
    message character varying(1000) NULL,
    created_at timestamp with time zone NULL,
    individual_delivery_time timestamp with time zone NULL,
    state character varying(30) NULL,
    payment_status character varying(30) NULL,
    payment_method character varying(50) NULL,
    is_returned boolean NOT NULL DEFAULT FALSE,
    request_date timestamp with time zone NULL,
    order_date timestamp with time zone NULL,
    time_for_accepting_request timestamp with time zone NULL,
    time_inform_customer_about_product timestamp with time zone NULL,
    amount_of_time_respond_client integer NULL,
    time_to_obtain_client_approval timestamp with time zone NULL,
    time_to_send_check_to_client timestamp with time zone NULL,
    request_processing_time timestamp with time zone NULL,
    comment_for_courier character varying(5000) NULL,
    reason_for_order_delay character varying(5000) NULL,
    reason_for_order_cancellation character varying(5000) NULL,
    reason_for_order_rejection character varying(5000) NULL,
    calling_at character varying(2000) NULL,
    long_search_reason character varying(5000) NULL,
    order_processing_time timestamp with time zone NULL,
    amount_of_processing_time integer NULL,
    amount_of_delivery_time integer NULL,
    delivered_time timestamp with time zone NULL,
    time_of_completion_of_inquiry timestamp with time zone NULL,
    reason_for_returning_the_order character varying(1000) NULL,
    returned_products_count integer NULL,
    was_rejection boolean NOT NULL DEFAULT FALSE,
    past_state character varying(30) NULL,
    delivered_at timestamp with time zone NULL,
    courier_accepted_at timestamp with time zone NULL,
    notified_at timestamp with time zone NULL,
    placed_at timestamp with time zone NULL,
    ready_for_shipment_at timestamp with time zone NULL,
    searching_at timestamp with time zone NULL,
    lead_created_at timestamp with time zone NULL,
    courier_received_at timestamp with time zone NULL,
    consulted_at timestamp with time zone NULL,
    placement_at timestamp with time zone NULL,
    canceled_at timestamp with time zone NULL,
    confirmed_at timestamp with time zone NULL,
    previous_state_before_rejection_state integer NULL,
    minutes_for_entire_process_finish integer NULL,
    minutes_for_entire_process_to_placement integer NULL,
    CONSTRAINT "PK_order_histories" PRIMARY KEY (id),
    CONSTRAINT "FK_order_histories_orders_order_id" FOREIGN KEY (order_id) REFERENCES orders (id) ON DELETE CASCADE
);

CREATE TABLE pharmacy_orders (
    id character varying(36) NOT NULL,
    order_id character varying(36) NOT NULL,
    pharmacy_id character varying(36) NULL,
    CONSTRAINT "PK_pharmacy_orders" PRIMARY KEY (id),
    CONSTRAINT "FK_pharmacy_orders_orders_order_id" FOREIGN KEY (order_id) REFERENCES orders (id) ON DELETE CASCADE,
    CONSTRAINT "FK_pharmacy_orders_pharmacies_pharmacy_id" FOREIGN KEY (pharmacy_id) REFERENCES pharmacies (id) ON DELETE CASCADE
);

CREATE TABLE courier_orders (
    id character varying(36) NOT NULL,
    courier_id character varying(36) NOT NULL,
    order_history_id character varying(36) NULL,
    CONSTRAINT "PK_courier_orders" PRIMARY KEY (id),
    CONSTRAINT "FK_courier_orders_order_histories_order_history_id" FOREIGN KEY (order_history_id) REFERENCES order_histories (id) ON DELETE SET NULL,
    CONSTRAINT "FK_courier_orders_users_courier_id" FOREIGN KEY (courier_id) REFERENCES users (id) ON DELETE CASCADE
);

CREATE TABLE operator_orders (
    id character varying(36) NOT NULL,
    operator_id character varying(36) NOT NULL,
    order_operator_id character varying(36) NULL,
    order_history_id character varying(36) NULL,
    CONSTRAINT "PK_operator_orders" PRIMARY KEY (id),
    CONSTRAINT "FK_operator_orders_order_histories_order_history_id" FOREIGN KEY (order_history_id) REFERENCES order_histories (id) ON DELETE SET NULL,
    CONSTRAINT "FK_operator_orders_users_operator_id" FOREIGN KEY (operator_id) REFERENCES users (id) ON DELETE CASCADE,
    CONSTRAINT "FK_operator_orders_users_order_operator_id" FOREIGN KEY (order_operator_id) REFERENCES users (id) ON DELETE SET NULL
);

CREATE TABLE product_histories (
    id character varying(36) NOT NULL,
    product_id character varying(36) NULL,
    pharmacy_order_id character varying(36) NOT NULL,
    message character varying(5000) NULL,
    long_search_reason character varying(5000) NULL,
    return_reason character varying(5000) NULL,
    is_returned boolean NOT NULL DEFAULT FALSE,
    returned_count integer NULL,
    count smallint NULL,
    amount_with_markup numeric(18,2) NULL,
    amount_without_markup numeric(18,2) NULL,
    created_at timestamp with time zone NULL,
    arrival_date timestamp with time zone NULL,
    return_to character varying(30) NULL,
    comment character varying(5000) NULL,
    CONSTRAINT "PK_product_histories" PRIMARY KEY (id),
    CONSTRAINT "FK_product_histories_pharmacy_orders_pharmacy_order_id" FOREIGN KEY (pharmacy_order_id) REFERENCES pharmacy_orders (id) ON DELETE CASCADE,
    CONSTRAINT "FK_product_histories_products_product_id" FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE
);

CREATE INDEX "IX_addresses_client_id" ON addresses (client_id);

CREATE INDEX "IX_categories_parent_id" ON categories (parent_id);

CREATE INDEX "IX_client_adults_client_id" ON client_adults (client_id);

CREATE INDEX "IX_client_childrens_client_id" ON client_childrens (client_id);

CREATE INDEX "IX_countries_order_id" ON countries (order_id);

CREATE INDEX "IX_courier_orders_courier_id" ON courier_orders (courier_id);

CREATE INDEX "IX_courier_orders_order_history_id" ON courier_orders (order_history_id);

CREATE INDEX "IX_operator_orders_operator_id" ON operator_orders (operator_id);

CREATE INDEX "IX_operator_orders_order_history_id" ON operator_orders (order_history_id);

CREATE INDEX "IX_operator_orders_order_operator_id" ON operator_orders (order_operator_id);

CREATE UNIQUE INDEX idx_order_histories_order_id ON order_histories (order_id);

CREATE INDEX idx_order_histories_state ON order_histories (state);

CREATE INDEX idx_orders_client_id ON orders (client_id);

CREATE INDEX idx_orders_order_number ON orders (order_number);

CREATE INDEX "IX_pharmacy_contacts_pharmacy_id" ON pharmacy_contacts (pharmacy_id);

CREATE INDEX "IX_pharmacy_orders_order_id" ON pharmacy_orders (order_id);

CREATE INDEX "IX_pharmacy_orders_pharmacy_id" ON pharmacy_orders (pharmacy_id);

CREATE INDEX "IX_product_histories_pharmacy_order_id" ON product_histories (pharmacy_order_id);

CREATE INDEX "IX_product_histories_product_id" ON product_histories (product_id);

CREATE INDEX "IX_product_providers_product_id" ON product_providers (product_id);

CREATE INDEX "IX_product_providers_provider_id" ON product_providers (provider_id);

CREATE INDEX "IX_product_templates_application_method_id" ON product_templates (application_method_id);

CREATE INDEX "IX_product_templates_category_id" ON product_templates (category_id);

CREATE INDEX "IX_product_templates_for_whom_id" ON product_templates (for_whom_id);

CREATE INDEX "IX_product_templates_organs_and_systems_id" ON product_templates (organs_and_systems_id);

CREATE INDEX "IX_product_templates_packaging_material_id" ON product_templates (packaging_material_id);

CREATE INDEX "IX_product_templates_preparation_color_id" ON product_templates (preparation_color_id);

CREATE INDEX "IX_product_templates_preparation_material_id" ON product_templates (preparation_material_id);

CREATE INDEX "IX_product_templates_scope_of_application_id" ON product_templates (scope_of_application_id);

CREATE INDEX "IX_product_templates_time_of_application_id" ON product_templates (time_of_application_id);

CREATE INDEX idx_products_name ON products (name);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260204122242_InitialCreate', '7.0.10');

COMMIT;

