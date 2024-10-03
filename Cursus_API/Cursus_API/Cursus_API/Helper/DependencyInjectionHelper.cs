using Cursus_Business.Common;
using Cursus_Business.Service.Implements;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using System.Configuration;

namespace Cursus_API.Helper
{
    public static class DependencyInjectionHelper
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserDetailRepository, UserDetailRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseVersionRepository, CourseVersionRepository>();
            services.AddScoped<ICourseVersionDetailRepository, CourseVersionDetailRepository>();
            services.AddScoped<ICourseContentRepository, CourseContentRepository>();
            services.AddScoped<ICourseCommentRepository, CourseCommentRepository>();
            services.AddScoped<IUserBehaviorRepository, UserBehaviorRepository>();
            services.AddScoped<IFinancialTransactionsRepository, FinancialTransactionsRepository>();
            services.AddScoped<ICourseVersionDetailRepository, CourseVersionDetailRepository>();
            services.AddScoped<ISystemTransactionRepository, SystemTransactionRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();

            services.AddScoped<IEmailTemplateRepsository,EmailTemplateRepsitory>();
            services.AddScoped<IMailRepository,MailRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            services.AddScoped<ICartRepository,CartRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IEnrollCourseRepository, EnrollCourseRepository>();


            services.AddScoped<IEnrolledCourseRepository, EnrolledCourseRepository>();
            services.AddScoped<IUserProcessRepository, UserProcessRepository>();
            services.AddScoped<IEmailTemplateRepsository,EmailTemplateRepsitory>();
            services.AddScoped<IMailRepository,MailRepository>();
            services.AddScoped<IBookmarkedRepository, BookmarkedRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();




            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseVersionService, CourseVersionService>();
            services.AddScoped<IUserDetailService, UserDetailService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<IFirebaseService, FirebaseService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseContentService, CourseContentService>();
            services.AddScoped<ICourseCommentService, CourseCommentService>();
            services.AddScoped<ICategoryService, CategoryService>();            
            services.AddScoped<IExcelExportService, ExcelExportService>();
            services.AddScoped<IMailServiceV2, MailServiceV2>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserBehaviorService, UserBehaviorService>();
            services.AddScoped<ICourseVersionDetailService, CourseVersionDetailService>();
            services.AddScoped<IMailServiceV3, MailServiceV3>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();

            services.AddScoped<IVnPaymentService, VnPaymentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartService, CartService>();


            services.AddScoped<IUserProcessService, UserProcessService>();
            services.AddScoped<IEnrolledCourseService, EnrolledCourseService>();
            services.AddScoped<IBookmarkedSerivce, BookmarkedSerivce>();


            services.AddFirebaseServices();
            services.AddControllers();
            services.AddHangfireServices(configuration);
            services.AddSingleton<ExcelTable>();
            return services;
        }
    }
}