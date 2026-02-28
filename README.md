# Market - Backend API

Backend لمشروع متجر BIM دمشق حسب الـ PRD: Clean Architecture، CQRS، PostgreSQL، JWT.

## الهيكل

- **BimMarket.Domain** – كيانات المجال (Branch, Category, Product, Order, …)
- **BimMarket.Application** – أوامر/استعلامات MediatR، DTOs، واجهات المستودعات
- **BimMarket.Infrastructure** – EF Core، PostgreSQL، Identity، تنفيذ المستودعات وخدمة JWT
- **BimMarket.API** – Controllers، Swagger، تسجيل الخدمات

## المتطلبات

- .NET 9 SDK
- PostgreSQL (مثلاً على localhost)

## الإعداد

1. **قاعدة البيانات**  
   عدّل سلسلة الاتصال في `src/BimMarket.API/appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Database=BimMarket;Username=postgres;Password=YOUR_PASSWORD"
   }
   ```

2. **JWT**  
   المفتاح الافتراضي موجود في `appsettings.json`. في الإنتاج غيّر `Jwt:Secret`.

3. **الهجرة الأولى**  
   تثبيت أدوات EF ثم إنشاء وتطبيق الهجرة:

   ```bash
   dotnet tool install --global dotnet-ef
   cd src/BimMarket.API
   dotnet ef migrations add Initial --project ../BimMarket.Infrastructure/BimMarket.Infrastructure.csproj
   dotnet ef database update --project ../BimMarket.Infrastructure/BimMarket.Infrastructure.csproj
   ```

4. **مستخدم أدمن للتجربة**  
   بعد تطبيق الهجرة، أنشئ دور "Admin" ومستخدمًا وربطه بالدور (من خلال كود أو سكربت)، أو استخدم واجهة Identity الافتراضية إن أضفتها.  
   مثال من Package Manager Console أو من سكربت:

   - إنشاء دور `Admin`
   - إنشاء مستخدم (Email + Password) وتعيين `UserName = Email`
   - ربط المستخدم بدور `Admin`

## التشغيل

```bash
cd src/BimMarket.API
dotnet run
```

- API: `http://localhost:5000`
- Swagger: `http://localhost:5000/swagger`

## نقاط نهاية تطبيق الأدمن (Android)

| Method | Endpoint | الوصف |
|--------|----------|--------|
| POST | `/api/auth/login` | تسجيل الدخول (Email, Password) |
| GET | `/api/admin/categories` | الفئات (مع أو بدون أطفال) |
| GET | `/api/admin/products` | المنتجات (صفحة، حجم، فئة، بحث) |
| GET | `/api/admin/products/{id}` | تفاصيل منتج |
| GET | `/api/admin/branches` | الفروع |
| GET | `/api/admin/inventory` | المخزون (فرع، منتج) |
| PUT | `/api/admin/inventory/{id}` | تحديث كمية وحد أدنى |
| GET | `/api/admin/orders` | الطلبات |
| GET | `/api/admin/orders/{id}` | تفاصيل طلب |
| PATCH | `/api/admin/orders/{id}/status` | تحديث حالة الطلب |
| GET | `/api/admin/coupons` | الكوبونات |
| GET | `/api/admin/flash-deals` | العروض الفورية |
| GET | `/api/admin/reviews` | التقييمات |
| PATCH | `/api/admin/reviews/{id}/approve` | الموافقة على تقييم |

جميع مسارات `/api/admin/*` تتطلب ترويسة: `Authorization: Bearer <access_token>`.

## ربط تطبيق الأدمن (Android)

في تطبيق الأدمن ضع عنوان الـ API كالتالي:

- إيموليتر: `http://10.0.2.2:5000`
- جهاز حقيقي على نفس الشبكة: `http://<IP_الكمبيوتر>:5000`
