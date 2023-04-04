using BPJ_U2T.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace BPJ_U2T.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderAccount> OrderAccounts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewImage> ReviewImages { get; set; }
        public DbSet<ProductDetailProcess> ProductDetailProcesses { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>()
               .HasData(
                   new Role { ID = 1, Name = "user" },
                   new Role { ID = 2, Name = "admin" }
               );

            builder.Entity<CategoryProduct>()
            .HasData(
                new CategoryProduct { Id = 1, Name = "ของกิน" },
                new CategoryProduct { Id = 2, Name = "ของใช้" }

            );
            //builder.Entity<District>()
            //.HasData(

            //    new District { ID = 1, Name = "ปากแพรก", TextDistrict = "ตำบลปากแพรกที่มาร่วมกิจกรรมปิดโครงการ U2T for BCG ตำบลปากแพรกวันนี้อย่างอุ่นหนาฝาคั่ง เกินความคาดหมาย หวังเป็นอย่างยิ่งว่าทุกคนจะได้นำความรู้ไปต่อยอดเพื่อเพิ่มรายได้ในครัวเรือน และขอบคุณทีมบัณฑิตและประชาชนตำบลปากแพรกที่ช่วยกันทำงานจนลุล่วงด้วยดี" },
            //    new District { ID = 2, Name = "แก่งเสี้ยน", TextDistrict = "ตำบลแก่งเสี้ยน อำเภอเมืองฯ จังหวัดกาญจนบุรี ในการดำเนินกิจกรรม ส่งมอบ น้ำยาฆ่าเชื้อ เจลแอลกอฮอล์ และหน้ากากอนามัยให้กับโรงเรียน วัด และหน่วยงานต่างๆในตำบล" },
            //    new District { ID = 3, Name = "ลาดหญ้า",TextDistrict = "ตำบลลาดหญ้าตั้งอยู่เลขที่ ๓๗๖ หมู่ ๑ ตำบลลาดหญ้า อำเภอเมืองกาญจนบุรี จังหวัด-กาญจนบุรี ได้รับการจัดตั้งเป็นสุขาภิบาลตามประกาศกระทรวงมหาดไทย เรื่องการจัดตั้งสุขาภิบาลและต่อมาได้รับการเปลี่ยนแปลงฐานะจากสุขาภิบาลเป็นเทศบาล ตามพระราชบัญญัติเปลี่ยนแปลงฐานะสุขาภิบาลเป็นเทศบาล" },
            //    new District { ID = 4, Name = "วังด้ง", TextDistrict = "ตำบลวังด้ง อำเภอเมืองฯ จังหวัดกาญจนบุรี ในการดำเนินกิจกรรม ส่งมอบ น้ำยาฆ่าเชื้อ เจลแอลกอฮอล์ และหน้ากากอนามัยให้กับโรงเรียน วัด และหน่วยงานต่างๆในตำบล" },
            //    new District { ID = 5, Name = "ท่าเสา", TextDistrict = "ตำบลท่าเสา กล้วยตากอินทรีย์ กล้วยตากเคลือบช็อคโกแลต กล้วยตากแฟนซี" },
            //    new District { ID = 6, Name = "หนองบัว", TextDistrict = "กลุ่ม U2T กระเป๋าสานบ้านป้า ตำบลหนองบัว เข้าร่วมออกบูทงาน KRU MARKETPLACE  ศูนย์จำหน่ายสินค้าคุณภาพ ภายใต้โครงการยกระดับมาตรฐานผลิตภัณฑ์ชุมชนท้องถิ่นเพื่อขยายตลาดภูมิปัญญา" },
            //    new District { ID = 7, Name = "ด่านแม่แฉลบ", TextDistrict = "ตำบลด่านแม่แฉลบ อำเภอศรีสวัสดิ์ จังหวัดกาญจนบุรี ร่วมกับองค์การบริหารส่วนตำบลดังกล่าว ในการดำเนินกิจกรรม ส่งมอบ น้ำยาฆ่าเชื้อ เจลแอลกอฮอล์ และหน้ากากอนามัยให้กับโรงเรียน วัด และหน่วยงานต่างๆในตำบล" },
            //    new District { ID = 8, Name = "ท่าม่วง", TextDistrict = "ทีม U2T for BCG ตำบลท่าม่วง ได้จัดโครงการขับเคลื่อนเศรษฐกิจและสังคมฐานรากหลังโควิดด้วยเศรษฐกิจ BCG (U2T for BCG) การพัฒนาและยกระดับผลิตภัณฑ์แปรรูปจากผ้าไหมเชิงพาณิชย์" },
            //    new District { ID = 9, Name = "บ้านเก่า", TextDistrict = "รองสถาพร ประสมทรัพย์ รองนายกองค์การบริหารส่วนตำบลบ้านเก่า ร่วมประชุมการรายงานผลการดำเนินงานและกิจกรรมที่จะดำเนินการต่อ ตามโครงการ U2T บ้านเก่า มหาวิทยาลัยราชภัฎกาญจนบุรี ผ่านสื่ออิเล็กทรอนิกส์" },
            //    new District { ID = 10, Name = "บ้านใหม่", TextDistrict = "ตำบลบ้านใหม่ อำเภอเมืองฯ จังหวัดกาญจนบุรี ในการดำเนินกิจกรรม ส่งมอบ น้ำยาฆ่าเชื้อ เจลแอลกอฮอล์ และหน้ากากอนามัยให้กับโรงเรียน วัด และหน่วยงานต่างๆในตำบล" },
            //    new District { ID = 11, Name = "ทุ่งทอง", TextDistrict = "ตำบลทุ่งทอง อำเภอเมืองฯ จังหวัดกาญจนบุรี ในการดำเนินกิจกรรม ส่งมอบ น้ำยาฆ่าเชื้อ เจลแอลกอฮอล์ และหน้ากากอนามัยให้กับโรงเรียน วัด และหน่วยงานต่างๆในตำบล" },
            //    new District { ID = 12, Name = "ท่าขนุน", TextDistrict = "ตำบลท่าขนุน มีโครงการมุ่งเน้น U2T for BCG ให้เป็นศูนย์วิจัยชุมชนด้านการท่องเที่ยวเชิงวิถี โดยสร้างกิจกรรมการท่องเที่ยวจากวิถีชุมชน เช่น การทำอาหารพื้นบ้าน (ขนมทองโยะ แกงยาดูร์ หมูเส้นสมุนไพร) การถ่อแพ การรำตงที่สอดแทรกประวัติศาสตร์ท้องถิ่น และผลผลิตทางการเกษตร เช่น ทุเรียนและเงาะทองผาภูม" },
            //    new District { ID = 13, Name = "ด่านมะขามเตี้ย" , TextDistrict = "ตำบลด่านมะขามเตี้ย อำเภอเมืองฯ จังหวัดกาญจนบุรี ในการดำเนินกิจกรรม ส่งมอบ น้ำยาฆ่าเชื้อ เจลแอลกอฮอล์ และหน้ากากอนามัยให้กับโรงเรียน วัด และหน่วยงานต่างๆในตำบล" },
            //    new District { ID = 14, Name = "กลอนโด",TextDistrict = "ทางทีมงาน U2Tตำบลกลอนโด และสมาชิกวิสาหกิจชุมชนบ้านย่านเจ้าพอเพียงได้เข้าร่วมถ่ายทอดการทำทองม้วนผำ" },
            //    new District { ID = 15, Name = "ตระคร้ำเอน" , TextDistrict = "ตำบลตะคร้ำเอนทั้งทีต้องร้านนี้ ร้านลูกชิ้นธรรมดาที่ไม่ธรรมดา ร้านลูกชิ้นไฟลุกหน้าวัดตะคร้ำเอน ขายมาแล้ว 70 ปี ปัจจุบันเป็นรุ่นที่ 3 สืบทอดมาจากบรรพบุรุษ เดิมคือลูกชิ้นยายน้อย ต่อมาพี่แม๊กได้มาดูแลกิจการต่อ มีให้เลือกมากมาย ทั้งลูกชิ้นชุบเเป้งทอด ลูกชิ้นทอด เกี๊ยวทอด เเละน้ำจิ้มสูตรเด็ด" }
            //);


        }
    }
}
