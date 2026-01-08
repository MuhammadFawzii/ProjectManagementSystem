using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Constants;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Infrastructure.Persistence;

namespace ProjectManagementSystem.Infrastructure.Seeders
{
    internal class ProjectManagementSeeder : IProjectManagementSeeder
    {
        private readonly ProjectManagementSystemDbContext _dbContext;

        public ProjectManagementSeeder(ProjectManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
            // Check if data already exists
            if (await _dbContext.Projects.AnyAsync())
            {
                return;
            }

            // Sample Owner IDs (In real scenario, these would be actual user IDs)
            var ownerId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var ownerId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var ownerId3 = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var ownerId4 = Guid.Parse("88888888-8888-8888-8888-888888888888");
            var ownerId5 = Guid.Parse("99999999-9999-9999-9999-999999999999");

            // Sample User IDs for task assignments
            var userId1 = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var userId2 = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var userId3 = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var userId4 = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var userId5 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var userId6 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var userId7 = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var userId8 = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

            var projects = new List<Project>
            {
                // Project 1: E-Commerce Platform
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "E-Commerce Platform Redesign",
                    Description = "Complete overhaul of the existing e-commerce platform with modern UI/UX and improved performance",
                    OwnerId = ownerId1,
                    CreatedAt = DateTime.UtcNow.AddMonths(-6),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-6),
                    ActualEndDate = null,
                    Budget = 150000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Design new product catalog UI",
                            Description = "Create mockups and prototypes for the new product catalog interface",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-6)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement shopping cart functionality",
                            Description = "Develop the shopping cart with add/remove items and quantity management",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-5)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Integrate payment gateway",
                            Description = "Connect Stripe payment gateway for secure transactions",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-4)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Setup inventory management system",
                            Description = "Implement real-time inventory tracking and alerts",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-3)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Develop order management dashboard",
                            Description = "Create admin dashboard for managing customer orders",
                            AssignedUserId = userId5,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-2)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement search and filtering",
                            Description = "Add advanced search with filters for categories, price range, and ratings",
                            AssignedUserId = userId6,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-1)
                        }
                    }
                },

                // Project 2: Mobile App Development
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Fitness Tracking Mobile App",
                    Description = "Cross-platform mobile application for tracking workouts, nutrition, and health metrics",
                    OwnerId = ownerId2,
                    CreatedAt = DateTime.UtcNow.AddMonths(-8),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-8),
                    ActualEndDate = null,
                    Budget = 80000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Setup React Native project",
                            Description = "Initialize the project structure and configure build pipelines",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-8)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement user authentication",
                            Description = "Add login, registration, and password reset functionality",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-7)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Build workout tracking feature",
                            Description = "Create UI for logging exercises, sets, reps, and weight",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-6)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Integrate health metrics API",
                            Description = "Connect with Apple Health and Google Fit APIs",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.Blocked,
                            CreatedAt = DateTime.UtcNow.AddMonths(-5)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Design nutrition tracking interface",
                            Description = "Create calorie and macro tracking UI with barcode scanner",
                            AssignedUserId = userId5,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-4)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement social features",
                            Description = "Add friend connections, activity feed, and challenges",
                            AssignedUserId = userId6,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-2)
                        }
                    }
                },

                // Project 3: Data Migration Project
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Legacy System Data Migration",
                    Description = "Migrate data from legacy SQL Server database to modern cloud-based solution",
                    OwnerId = ownerId3,
                    CreatedAt = DateTime.UtcNow.AddMonths(-4),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-4),
                    ActualEndDate = DateTime.UtcNow.AddMonths(-1),
                    Budget = 50000m,
                    Currency = "EUR",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Analyze legacy database schema",
                            Description = "Document existing database structure and relationships",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-4)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Design new database schema",
                            Description = "Create optimized schema for cloud database",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-3)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Develop ETL pipeline",
                            Description = "Build extraction, transformation, and loading processes",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-2)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Execute data migration",
                            Description = "Run the migration process and validate data integrity",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-1)
                        }
                    }
                },

                // Project 4: Internal Dashboard
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Company Analytics Dashboard",
                    Description = "Internal dashboard for visualizing company metrics, KPIs, and reports",
                    OwnerId = ownerId1,
                    CreatedAt = DateTime.UtcNow.AddMonths(-3),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-3),
                    ActualEndDate = null,
                    Budget = 35000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Gather requirements from stakeholders",
                            Description = "Interview department heads to understand their reporting needs",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-3)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Setup data warehouse connection",
                            Description = "Configure secure connection to the company data warehouse",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-2)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Create dashboard wireframes",
                            Description = "Design layout and widget placement for the dashboard",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddDays(-45)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement real-time data updates",
                            Description = "Setup SignalR for live dashboard updates",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddDays(-30)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Build sales performance widgets",
                            Description = "Create charts and graphs for sales metrics",
                            AssignedUserId = userId5,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-15)
                        }
                    }
                },

                // Project 5: API Development
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "REST API for Partner Integration",
                    Description = "Develop public API for third-party partners to integrate with our services",
                    OwnerId = ownerId2,
                    CreatedAt = DateTime.UtcNow.AddMonths(-5),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-5),
                    ActualEndDate = null,
                    Budget = 65000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Design API specification",
                            Description = "Create OpenAPI/Swagger specification for all endpoints",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-5)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement authentication layer",
                            Description = "Add OAuth2 authentication for API access",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-4)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Develop core endpoints",
                            Description = "Build CRUD operations for main resources",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-3)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Setup rate limiting",
                            Description = "Implement rate limiting to prevent API abuse",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-2)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Write API documentation",
                            Description = "Create comprehensive documentation with examples",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-1)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement webhook notifications",
                            Description = "Add webhook support for event notifications",
                            AssignedUserId = userId5,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-20)
                        }
                    }
                },

                // Project 6: DevOps Infrastructure
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "DevOps Pipeline Modernization",
                    Description = "Upgrade CI/CD pipelines and implement infrastructure as code",
                    OwnerId = ownerId3,
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-2),
                    ActualEndDate = null,
                    Budget = 45000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Audit existing CI/CD processes",
                            Description = "Review current deployment pipelines and identify bottlenecks",
                            AssignedUserId = userId7,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-2)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Setup Terraform infrastructure",
                            Description = "Convert infrastructure to Terraform configuration",
                            AssignedUserId = userId8,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddDays(-45)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Migrate to GitHub Actions",
                            Description = "Replace Jenkins pipelines with GitHub Actions workflows",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddDays(-30)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement automated testing",
                            Description = "Add unit, integration, and E2E tests to pipeline",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-15)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Setup monitoring and alerts",
                            Description = "Configure Prometheus and Grafana for system monitoring",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-10)
                        }
                    }
                },

                // Project 7: Machine Learning Initiative
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Customer Behavior Prediction Model",
                    Description = "Build ML model to predict customer churn and purchasing patterns",
                    OwnerId = ownerId4,
                    CreatedAt = DateTime.UtcNow.AddMonths(-7),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-7),
                    ActualEndDate = null,
                    Budget = 95000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Collect and clean training data",
                            Description = "Gather historical customer data and prepare for model training",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-7)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Feature engineering",
                            Description = "Identify and create relevant features for the prediction model",
                            AssignedUserId = userId5,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-6)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Train initial models",
                            Description = "Experiment with different algorithms (Random Forest, XGBoost, Neural Networks)",
                            AssignedUserId = userId6,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-5)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Hyperparameter tuning",
                            Description = "Optimize model parameters for best performance",
                            AssignedUserId = userId7,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-4)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Deploy model to production",
                            Description = "Setup model serving infrastructure with API endpoint",
                            AssignedUserId = userId8,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-3)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement model monitoring",
                            Description = "Track model performance and detect drift",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-2)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Create prediction dashboard",
                            Description = "Build visualization dashboard for predictions and insights",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-1)
                        }
                    }
                },

                // Project 8: Security Audit
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Application Security Hardening",
                    Description = "Comprehensive security audit and implementation of security best practices",
                    OwnerId = ownerId5,
                    CreatedAt = DateTime.UtcNow.AddDays(-60),
                    ExpectedStartDate = DateTime.UtcNow.AddDays(-60),
                    ActualEndDate = null,
                    Budget = 30000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Perform penetration testing",
                            Description = "Conduct thorough security testing to identify vulnerabilities",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddDays(-60)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement OWASP Top 10 fixes",
                            Description = "Address common security vulnerabilities from OWASP list",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddDays(-45)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Setup security scanning in CI/CD",
                            Description = "Integrate SAST and DAST tools into deployment pipeline",
                            AssignedUserId = userId5,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddDays(-30)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement MFA for all users",
                            Description = "Add multi-factor authentication support",
                            AssignedUserId = userId6,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-20)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Encrypt sensitive data at rest",
                            Description = "Implement encryption for PII and sensitive information",
                            AssignedUserId = userId7,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-15)
                        }
                    }
                },

                // Project 9: Mobile Game Development
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Puzzle Adventure Mobile Game",
                    Description = "Casual mobile puzzle game with social features and in-app purchases",
                    OwnerId = ownerId4,
                    CreatedAt = DateTime.UtcNow.AddMonths(-10),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-10),
                    ActualEndDate = null,
                    Budget = 120000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Design game mechanics",
                            Description = "Create core gameplay loop and puzzle mechanics",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-10)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Create art assets",
                            Description = "Design characters, backgrounds, and UI elements",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-9)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement level progression system",
                            Description = "Build level unlock mechanism and difficulty scaling",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-8)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Add sound effects and music",
                            Description = "Compose background music and create sound effects",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-7)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement in-app purchases",
                            Description = "Setup monetization with coins and power-ups",
                            AssignedUserId = userId5,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-6)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Add leaderboard system",
                            Description = "Implement global and friend leaderboards",
                            AssignedUserId = userId6,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-5)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Beta testing phase",
                            Description = "Conduct closed beta testing and gather feedback",
                            AssignedUserId = userId7,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-4)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Optimize performance",
                            Description = "Reduce load times and improve frame rate",
                            AssignedUserId = userId8,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-3)
                        }
                    }
                },

                // Project 10: CRM System
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Customer Relationship Management System",
                    Description = "Internal CRM for managing customer interactions, sales pipeline, and support tickets",
                    OwnerId = ownerId5,
                    CreatedAt = DateTime.UtcNow.AddMonths(-4),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-4),
                    ActualEndDate = null,
                    Budget = 85000m,
                    Currency = "EUR",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Design database schema",
                            Description = "Model customers, contacts, deals, and activities",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-4)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Build contact management module",
                            Description = "CRUD operations for customers and contacts",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-3)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement sales pipeline",
                            Description = "Create deal stages and opportunity tracking",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddDays(-60)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Add email integration",
                            Description = "Sync with Gmail and Outlook for email tracking",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddDays(-45)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Build reporting dashboard",
                            Description = "Sales metrics, conversion rates, and team performance",
                            AssignedUserId = userId5,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-30)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement task automation",
                            Description = "Automated workflows for follow-ups and reminders",
                            AssignedUserId = userId6,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-20)
                        }
                    }
                },

                // Project 11: Blockchain Integration
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "NFT Marketplace Platform",
                    Description = "Decentralized marketplace for buying, selling, and trading digital art NFTs",
                    OwnerId = ownerId1,
                    CreatedAt = DateTime.UtcNow.AddMonths(-5),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-5),
                    ActualEndDate = null,
                    Budget = 175000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Research blockchain platforms",
                            Description = "Evaluate Ethereum, Polygon, and Solana for NFT support",
                            AssignedUserId = userId7,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-5)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Develop smart contracts",
                            Description = "Write and test ERC-721 contracts for NFT minting",
                            AssignedUserId = userId8,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-4)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Build marketplace frontend",
                            Description = "Create UI for browsing, searching, and purchasing NFTs",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-3)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Integrate wallet connections",
                            Description = "Support MetaMask, WalletConnect, and Coinbase Wallet",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-2)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement IPFS storage",
                            Description = "Store NFT metadata and images on IPFS",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.Blocked,
                            CreatedAt = DateTime.UtcNow.AddMonths(-1)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Add auction functionality",
                            Description = "Build timed auctions with bid tracking",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-25)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Setup royalty payments",
                            Description = "Implement automatic royalty distribution to creators",
                            AssignedUserId = userId5,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-15)
                        }
                    }
                },

                // Project 12: Small Completed Project
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Website Maintenance and Bug Fixes",
                    Description = "Monthly maintenance tasks and bug fixes for company website",
                    OwnerId = ownerId2,
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-2),
                    ActualEndDate = DateTime.UtcNow.AddDays(-5),
                    Budget = 5000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Fix contact form validation",
                            Description = "Resolve issues with email validation on contact form",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-2)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Update SSL certificates",
                            Description = "Renew expiring SSL certificates for all domains",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddDays(-20)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Optimize image loading",
                            Description = "Compress images and implement lazy loading",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddDays(-10)
                        }
                    }
                },

                // Project 13: IoT Platform
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Smart Home IoT Platform",
                    Description = "Platform for managing and controlling smart home devices",
                    OwnerId = ownerId3,
                    CreatedAt = DateTime.UtcNow.AddMonths(-9),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-9),
                    ActualEndDate = null,
                    Budget = 110000m,
                    Currency = "GBP",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Design IoT device protocol",
                            Description = "Define MQTT message structure for device communication",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-9)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Build device management API",
                            Description = "Create REST API for device registration and control",
                            AssignedUserId = userId5,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-8)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Develop mobile control app",
                            Description = "iOS and Android app for controlling devices",
                            AssignedUserId = userId6,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-7)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement automation rules",
                            Description = "Create rule engine for device automation scenarios",
                            AssignedUserId = userId7,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddMonths(-6)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Add voice assistant integration",
                            Description = "Support Alexa and Google Assistant voice commands",
                            AssignedUserId = userId8,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-5)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Setup edge computing nodes",
                            Description = "Deploy edge servers for local device processing",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddMonths(-4)
                        }
                    }
                },

                // Project 14: Cancelled Project
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Social Media Integration Suite",
                    Description = "Cross-platform social media management and analytics tool",
                    OwnerId = ownerId4,
                    CreatedAt = DateTime.UtcNow.AddMonths(-6),
                    ExpectedStartDate = DateTime.UtcNow.AddMonths(-6),
                    ActualEndDate = DateTime.UtcNow.AddMonths(-4),
                    Budget = 40000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Research social media APIs",
                            Description = "Evaluate Twitter, Facebook, Instagram, LinkedIn APIs",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddMonths(-6)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Build authentication module",
                            Description = "OAuth integration for all platforms",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.Cancelled,
                            CreatedAt = DateTime.UtcNow.AddMonths(-5)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Create posting interface",
                            Description = "UI for creating and scheduling posts",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.Cancelled,
                            CreatedAt = DateTime.UtcNow.AddMonths(-5)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement analytics dashboard",
                            Description = "Aggregate metrics from all connected platforms",
                            AssignedUserId = userId5,
                            Status = ProjectTaskStatus.Cancelled,
                            CreatedAt = DateTime.UtcNow.AddMonths(-4)
                        }
                    }
                },

                // Project 15: Video Streaming
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Live Video Streaming Platform",
                    Description = "Platform for hosting live streams and video on demand content",
                    OwnerId = ownerId5,
                    CreatedAt = DateTime.UtcNow.AddDays(-90),
                    ExpectedStartDate = DateTime.UtcNow.AddDays(-90),
                    ActualEndDate = null,
                    Budget = 200000m,
                    Currency = "USD",
                    Tasks = new List<ProjectTask>
                    {
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Setup video encoding infrastructure",
                            Description = "Configure FFmpeg and transcoding servers",
                            AssignedUserId = userId6,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddDays(-90)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement HLS/DASH streaming",
                            Description = "Setup adaptive bitrate streaming protocols",
                            AssignedUserId = userId7,
                            Status = ProjectTaskStatus.Completed,
                            CreatedAt = DateTime.UtcNow.AddDays(-75)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Build video player interface",
                            Description = "Custom video player with quality selection and controls",
                            AssignedUserId = userId8,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddDays(-60)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Add chat functionality",
                            Description = "Real-time chat for live stream viewers",
                            AssignedUserId = userId1,
                            Status = ProjectTaskStatus.InProgress,
                            CreatedAt = DateTime.UtcNow.AddDays(-45)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Implement CDN integration",
                            Description = "Setup CloudFlare Stream or AWS CloudFront",
                            AssignedUserId = userId2,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-30)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Add monetization options",
                            Description = "Subscription tiers, pay-per-view, and donations",
                            AssignedUserId = userId3,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-20)
                        },
                        new ProjectTask
                        {
                            Id = Guid.NewGuid(),
                            Title = "Build content moderation tools",
                            Description = "Automated and manual moderation for live content",
                            AssignedUserId = userId4,
                            Status = ProjectTaskStatus.NotStarted,
                            CreatedAt = DateTime.UtcNow.AddDays(-10)
                        }
                    }
                }
            };

            await _dbContext.Projects.AddRangeAsync(projects);
            await _dbContext.SaveChangesAsync();
        }
    }
}
