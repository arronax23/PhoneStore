using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneStore.DAL.Migrations
{
    public partial class SeedPhones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Apple',12,1,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/32/3225242/APPLE-iPhone-13-Mini-Czarny-01.jpg',512,'iPhone 13','iOS 15',4, 200)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Oppo',64,2,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/31/3110508/Smartfon-OPPO-Reno-6-Czarny-front-tyl.jpg',128,'Reno 6','Android 11',8,350)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Samsung',12,1,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/30/3060352/Smartfon-SAMSUNG-Galaxy-Z-Flip-3-Bezowy-front_plecki-Bezowy.jpg',128,'Galaxy Z Flip','Android 11',8,400)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Xiaomi',64,3,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/29/2960578/Smartfon-XIAOMI-Mi-11-Lite-6-128GB-6.55-90Hz-Niebieski-31440-tyl-front.jpg',128,'Mi 11 Lite','Android 11',6,500)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Samsung',12,1,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/30/3060348/Smartfon-SAMSUNG-Galaxy-Z-Fold-3-front.jpg',256,'Galaxy Z Fold','Android 11',12,505)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Xiaomi',48,1,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/29/2927116/Smartfon-XIAOMI-Redmi-9T-Szary-front-tyl-1.jpg',128,'Redmi 9T','Android 10',4,259)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Realme',108,3,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/28/2893937/Smartfon-REALME-8-Pro-8-128GB-Niebieski-front-tyl.jpg',128,'8 Pro','Android 11',8,650)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Nokia',13,3,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/26/2642085/Smartfon-NOKIA-3-4-Dual-Sim-Niebieski-front-tyl.jpg',64,'3.4','Android 10',3,150)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Samsung',64,1,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/32/3209328/Smartfon-SAMSUNG-Galaxy-M52-Czarny-front-tyl.jpg',128,'Galaxy M52','Android 11',6,400)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Apple',12,1,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/26/2607377/Smartfon-APPLE-iPhone-12-mini-64GB-Czarny-front.jpg',124,'iPhone 12 mini','iOS 14',5,600)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Xiaomi',108,3,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/32/3248922/Smartfon-XIAOMI-11T-5G-Niebieski-tyl-front.jpg',128,'11T','Android 11',8,700)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Samsung',32,3,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/29/2900755/Smartfon-SAMSUNG-Galaxy-S20-FE-SM-G780-Niebieski-tyl-front.jpg',128,'Galaxy S20','Android 10',6,300)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Realme',64,0,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/31/3105274/Smartfon-REALME-GT-Master-Edition-Bialy-front-tyl.jpg',128,'GT Master Edition','Android 11',6,360)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Samsung',16,1,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/27/2786851/Smartfon-SAMSUNG-Galaxy-XCover-5-4-64-GB-Czarny-SM-G525-tyl-front.jpg',64,'Galaxy','Android 11',4,545)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Realme',48,0,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/26/2622353/Smartfon-REALME-7i-4-64GB-Srebrny-front-tyl.jpg',64,'7i','Android 10',4,450)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Sony',12,1,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/30/3049852/Smartfon-SONY-Xperia-1-III-5G-Czarny-tyl-front.jpg',256,'Xperia1','Android 11',12,445)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Apple',12,3,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/26/2607211/Smartfon-APPLE-iPhone-12-Pro-Max-5G-Srebrny-1.jpg',128,'iPhone 12','iOS 14',6,435)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Oukitel',13,3,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/25/2521672/Smartfon-OUKITEL-C19-2-16GB-Niebieski-tyl-front.jpg',16,'C19','Android 10 Go',2,330)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Oppo',13,0,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/26/2689191/Smartfon-OPPO-A15s-4-64GB-Czarny-front-tyl.jpg',64,'A15S','Android 11',4,210)
                                INSERT INTO Phones([Brand], [Camera], [Color], [ImageUrl], [Memory], [Model], [OS],  [RAM], [Price]) VALUES ('Ulefone',12,1,'https://prod-api.mediaexpert.pl/api/images/gallery_500_500/thumbnails/images/26/2617121/Smartfon-ULEFONE-Armor-8-4-64GB-Pomaranczowy-front-tyl.jpg',64,'Armor 8','Android 10',4,340)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
