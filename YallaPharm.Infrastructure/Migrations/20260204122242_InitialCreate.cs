using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YallaPharm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "application_methods",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    application_method = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    parent_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_categories_categories_parent_id",
                        column: x => x.parent_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    full_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    street = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    landmark = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    geolocation_of_client_address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    age = table.Column<short>(type: "smallint", nullable: true),
                    social_username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    language = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    having_children = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    having_elderly = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    family_status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    economic_standing = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    childrens_age = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    contact_type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    take_into_account = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    discount_for_client = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "for_whom",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    for_whom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_for_whom", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organs_and_systems",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    organs_and_system = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organs_and_systems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "packaging_materials",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    packaging_material = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packaging_materials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "packaging_types",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packaging_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "packaging_units",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packaging_units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pharmacies",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    landmark = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    contact = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    geolocation_link = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    markup = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    markup_type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    is_required = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_abroad = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    payout_method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pharmacies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "preparation_colors",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    preparation_color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preparation_colors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "preparation_materials",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    preparation_material = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preparation_materials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    dosage = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    count_on_package = table.Column<int>(type: "integer", nullable: true),
                    age_from = table.Column<int>(type: "integer", nullable: true),
                    age_to = table.Column<int>(type: "integer", nullable: true),
                    price_with_markup = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    price_without_markup = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    manufacturer = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    path_image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    dificit = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    release_form = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    packaging_unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    type_of_packaging = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    link_product = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    country = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    age_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    is_required = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    state = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "providers",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    contact_type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    contact = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    link_from_where_found_abroad = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_providers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "release_forms",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_release_forms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scope_of_applications",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    scope_of_application = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scope_of_applications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "time_of_applications",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    time_of_application = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time_of_applications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    first_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    last_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    client_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    street = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    landmark = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    geolocation_of_client_address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.id);
                    table.ForeignKey(
                        name: "FK_addresses_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_adults",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    client_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    age = table.Column<short>(type: "smallint", nullable: true),
                    gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_adults", x => x.id);
                    table.ForeignKey(
                        name: "FK_client_adults_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_childrens",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    client_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    age = table.Column<short>(type: "smallint", nullable: true),
                    gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_childrens", x => x.id);
                    table.ForeignKey(
                        name: "FK_client_childrens_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    client_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    order_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    comment = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    country_to_delivery = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    courier = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    comment_for_courier = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    @operator = table.Column<string>(name: "operator", type: "character varying(100)", maxLength: 100, nullable: false),
                    total_cost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    prepayment = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    rest_payment = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    discount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    total_order_amount_excluding_delivery = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    city_or_district = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    price_for_delivery_outside_the_city = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    remaining_payment = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    amount_with_discount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    amount_with_markup = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    amount_without_markup = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    amount_with_delivery = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    amount_without_delivery = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    comes_from = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    delivery_type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    delivered_at = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pharmacy_contacts",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    pharmacy_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    contact = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    contact_type = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pharmacy_contacts", x => x.id);
                    table.ForeignKey(
                        name: "FK_pharmacy_contacts_pharmacies_pharmacy_id",
                        column: x => x.pharmacy_id,
                        principalTable: "pharmacies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_providers",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    product_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    provider_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_providers", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_providers_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_providers_providers_provider_id",
                        column: x => x.provider_id,
                        principalTable: "providers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_templates",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    category_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    nickname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    active_ingredient = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    dosage = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    dosage_unit = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    minimum_quantity_per_piece = table.Column<int>(type: "integer", nullable: true),
                    pack_quantity_or_drug_volume = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    packaging_unit = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    preparation_taste = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    age_from = table.Column<short>(type: "smallint", nullable: true),
                    age_to = table.Column<short>(type: "smallint", nullable: true),
                    instructions = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    indication_for_use = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    contraindication_for_use = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    symptom = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    for_allergy_sufferers = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    for_diabetics = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    for_pregnant_women = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    for_children = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    for_driver = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    season_of_application = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    dried_fruit = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    comment = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    with_caution = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    vacation_condition = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    for_whom_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    application_method_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    organs_and_systems_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    packaging_material_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    preparation_color_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    preparation_material_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    scope_of_application_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    time_of_application_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_templates", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_templates_application_methods_application_method_id",
                        column: x => x.application_method_id,
                        principalTable: "application_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_templates_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_templates_for_whom_for_whom_id",
                        column: x => x.for_whom_id,
                        principalTable: "for_whom",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_templates_organs_and_systems_organs_and_systems_id",
                        column: x => x.organs_and_systems_id,
                        principalTable: "organs_and_systems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_product_templates_packaging_materials_packaging_material_id",
                        column: x => x.packaging_material_id,
                        principalTable: "packaging_materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_templates_preparation_colors_preparation_color_id",
                        column: x => x.preparation_color_id,
                        principalTable: "preparation_colors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_templates_preparation_materials_preparation_materia~",
                        column: x => x.preparation_material_id,
                        principalTable: "preparation_materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_templates_scope_of_applications_scope_of_applicatio~",
                        column: x => x.scope_of_application_id,
                        principalTable: "scope_of_applications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_product_templates_time_of_applications_time_of_application_~",
                        column: x => x.time_of_application_id,
                        principalTable: "time_of_applications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    order_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                    table.ForeignKey(
                        name: "FK_countries_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_histories",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    order_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    individual_delivery_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    state = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    payment_status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    payment_method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_returned = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    request_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    time_for_accepting_request = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    time_inform_customer_about_product = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    amount_of_time_respond_client = table.Column<int>(type: "integer", nullable: true),
                    time_to_obtain_client_approval = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    time_to_send_check_to_client = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    request_processing_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    comment_for_courier = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    reason_for_order_delay = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    reason_for_order_cancellation = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    reason_for_order_rejection = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    calling_at = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    long_search_reason = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    order_processing_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    amount_of_processing_time = table.Column<int>(type: "integer", nullable: true),
                    amount_of_delivery_time = table.Column<int>(type: "integer", nullable: true),
                    delivered_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    time_of_completion_of_inquiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    reason_for_returning_the_order = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    returned_products_count = table.Column<int>(type: "integer", nullable: true),
                    was_rejection = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    past_state = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    delivered_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    courier_accepted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    notified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    placed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ready_for_shipment_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    searching_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    lead_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    courier_received_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    consulted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    placement_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    canceled_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    confirmed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    previous_state_before_rejection_state = table.Column<int>(type: "integer", nullable: true),
                    minutes_for_entire_process_finish = table.Column<int>(type: "integer", nullable: true),
                    minutes_for_entire_process_to_placement = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_histories", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_histories_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pharmacy_orders",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    order_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    pharmacy_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pharmacy_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_pharmacy_orders_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pharmacy_orders_pharmacies_pharmacy_id",
                        column: x => x.pharmacy_id,
                        principalTable: "pharmacies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "courier_orders",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    courier_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    order_history_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courier_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_courier_orders_order_histories_order_history_id",
                        column: x => x.order_history_id,
                        principalTable: "order_histories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_courier_orders_users_courier_id",
                        column: x => x.courier_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operator_orders",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    operator_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    order_operator_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    order_history_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operator_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_operator_orders_order_histories_order_history_id",
                        column: x => x.order_history_id,
                        principalTable: "order_histories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_operator_orders_users_operator_id",
                        column: x => x.operator_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_operator_orders_users_order_operator_id",
                        column: x => x.order_operator_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "product_histories",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    product_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    pharmacy_order_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    message = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    long_search_reason = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    return_reason = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    is_returned = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    returned_count = table.Column<int>(type: "integer", nullable: true),
                    count = table.Column<short>(type: "smallint", nullable: true),
                    amount_with_markup = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    amount_without_markup = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    arrival_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    return_to = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    comment = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_histories", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_histories_pharmacy_orders_pharmacy_order_id",
                        column: x => x.pharmacy_order_id,
                        principalTable: "pharmacy_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_histories_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_client_id",
                table: "addresses",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_parent_id",
                table: "categories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_adults_client_id",
                table: "client_adults",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_childrens_client_id",
                table: "client_childrens",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_countries_order_id",
                table: "countries",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_courier_orders_courier_id",
                table: "courier_orders",
                column: "courier_id");

            migrationBuilder.CreateIndex(
                name: "IX_courier_orders_order_history_id",
                table: "courier_orders",
                column: "order_history_id");

            migrationBuilder.CreateIndex(
                name: "IX_operator_orders_operator_id",
                table: "operator_orders",
                column: "operator_id");

            migrationBuilder.CreateIndex(
                name: "IX_operator_orders_order_history_id",
                table: "operator_orders",
                column: "order_history_id");

            migrationBuilder.CreateIndex(
                name: "IX_operator_orders_order_operator_id",
                table: "operator_orders",
                column: "order_operator_id");

            migrationBuilder.CreateIndex(
                name: "idx_order_histories_order_id",
                table: "order_histories",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_order_histories_state",
                table: "order_histories",
                column: "state");

            migrationBuilder.CreateIndex(
                name: "idx_orders_client_id",
                table: "orders",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "idx_orders_order_number",
                table: "orders",
                column: "order_number");

            migrationBuilder.CreateIndex(
                name: "IX_pharmacy_contacts_pharmacy_id",
                table: "pharmacy_contacts",
                column: "pharmacy_id");

            migrationBuilder.CreateIndex(
                name: "IX_pharmacy_orders_order_id",
                table: "pharmacy_orders",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_pharmacy_orders_pharmacy_id",
                table: "pharmacy_orders",
                column: "pharmacy_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_histories_pharmacy_order_id",
                table: "product_histories",
                column: "pharmacy_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_histories_product_id",
                table: "product_histories",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_providers_product_id",
                table: "product_providers",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_providers_provider_id",
                table: "product_providers",
                column: "provider_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_templates_application_method_id",
                table: "product_templates",
                column: "application_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_templates_category_id",
                table: "product_templates",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_templates_for_whom_id",
                table: "product_templates",
                column: "for_whom_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_templates_organs_and_systems_id",
                table: "product_templates",
                column: "organs_and_systems_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_templates_packaging_material_id",
                table: "product_templates",
                column: "packaging_material_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_templates_preparation_color_id",
                table: "product_templates",
                column: "preparation_color_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_templates_preparation_material_id",
                table: "product_templates",
                column: "preparation_material_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_templates_scope_of_application_id",
                table: "product_templates",
                column: "scope_of_application_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_templates_time_of_application_id",
                table: "product_templates",
                column: "time_of_application_id");

            migrationBuilder.CreateIndex(
                name: "idx_products_name",
                table: "products",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "client_adults");

            migrationBuilder.DropTable(
                name: "client_childrens");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "courier_orders");

            migrationBuilder.DropTable(
                name: "operator_orders");

            migrationBuilder.DropTable(
                name: "packaging_types");

            migrationBuilder.DropTable(
                name: "packaging_units");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "pharmacy_contacts");

            migrationBuilder.DropTable(
                name: "product_histories");

            migrationBuilder.DropTable(
                name: "product_providers");

            migrationBuilder.DropTable(
                name: "product_templates");

            migrationBuilder.DropTable(
                name: "release_forms");

            migrationBuilder.DropTable(
                name: "order_histories");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "pharmacy_orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "providers");

            migrationBuilder.DropTable(
                name: "application_methods");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "for_whom");

            migrationBuilder.DropTable(
                name: "organs_and_systems");

            migrationBuilder.DropTable(
                name: "packaging_materials");

            migrationBuilder.DropTable(
                name: "preparation_colors");

            migrationBuilder.DropTable(
                name: "preparation_materials");

            migrationBuilder.DropTable(
                name: "scope_of_applications");

            migrationBuilder.DropTable(
                name: "time_of_applications");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "pharmacies");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
