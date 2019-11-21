using System.Linq;
using Store.Database.Entities;

namespace Store.Database.EF
{
    public static class DbInitializer
    {

        public static void Initialize(DefaultContext db)
        {
            //db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            AddProducts(db);

            db.SaveChanges();
        }

        private static void AddProducts(DefaultContext db)
        {
            if (db.Products.Any())
                return;

            db.Products.Add(new Product 
                {Name = "Карамель Chupa Chups MINI",  
                Description= "Чупа Чупс Мини - любимая карамель на палочке Чупа Чупс в миниформате. " +
                "В шоубоксе 100 Чупа Чупсов с лучшими фруктовыми вкусами: " +
                "клубника,кола,апельсин,яблоко.", 
                PhotoUrl = "product1", Price = 31});

            db.Products.Add(new Product
            { Name = "Батончики Рот Фронт", 
                Description = "Наверное, эти конфеты пробовал каждый: батончики давно стали классикой. " +
                "Нежные конфеты с арахисовым вкусом особенно хороши к кофе, сваренному в турке.", 
                PhotoUrl = "product2", Price = 32 });

            db.Products.Add(new Product
            { Name = "Конфеты Александровские Коровки", Description = "Сливочные конфеты аппетитного карамельного цвета ? это идеальный вариант для закуски к чаю или кофе", 
                PhotoUrl = "product3", Price = 42 });

            db.Products.Add(new Product
            { Name = "Конфеты Рот Фронт Птичье молоко", Description = "Под шоколадной глазурью — нежное суфле с едва уловимой кислинкой и сливочным ароматом!", PhotoUrl = "product4", Price = 23 });

            db.Products.Add(new Product
            { Name = "Батончик Snickers Minis", Description = "Батончик Snickers Minis с жареным арахисом, карамелью и нугой, покрытый молочным шоколадом", PhotoUrl = "product5", Price = 54 });

            db.Products.Add(new Product
            { Name = "Шоколадные конфеты А.Коркунов Ассорти из темного и молочного шоколада", 
                Description = "Набор шоколадных конфет Ассорти из молочного и темного шоколада ? изысканное сочетание десертов от лучших российских шоколатье. Он может стать идеальным подарком для близких или украшением праздничного стола", 
                PhotoUrl = "product6", Price = 65 });

            db.Products.Add(new Product
            { Name = "Шоколадный набор Комильфо фисташка", Description = "Внутри тонкого «стаканчика» из темного шоколада — фисташковый мусс и сливочный крем, а сверху — хрустящая фисташка. Оцените изысканное сочетание! Каждая конфета «Комильфо» — неповторимое сочетание нежной текстуры, восхитительных начинок, превосходного шоколада и акцента в виде небольшого украшения.", 
                PhotoUrl = "product7", Price = 535 });

            db.Products.Add(new Product
            { Name = "Драже M&M's Криспи", Description = "M&M's «Криспи» это вкусные драже в разноцветной оболочке с начинкой из молочного шоколада и маленьких хрустящих гранул, которые называются Cryspi", 
                PhotoUrl = "product8", Price = 24 });

            db.Products.Add(new Product
            { Name = "Конфеты Красный Октябрь Москва", Description = "Конфеты из мягкой карамели, сладкие, но не приторные, отлично сочетаются как с чаем, так и с кофе", PhotoUrl = "product9", Price = 656 });

            db.Products.Add(new Product
            { Name = "Батончики Рот Фронт шоколадно-сливочный вкус", Description = "Шоколадно-сливочные батончики — для тех, кто особенно любит шоколад! Нежное арахисовое пралине дополнено ароматным какао", PhotoUrl = "product10", Price = 76 });

            db.Products.Add(new Product
            { Name = "Конфеты Красный Октябрь Маска", Description = "Яркие конфеты сочетание шоколадного пралине и шоколадной глазури. Разноцветные маски на фантике уже давно ассоциируются с праздничным чаепитием!", PhotoUrl = "product11", Price = 45 });

            db.Products.Add(new Product
            { Name = "Клюква Ударница Шармэль в сахарной пудре", Description = "Любимое с самых ранних лет угощение — спелая кислая клюква в сладкой сахарной пудре!", PhotoUrl = "product12", Price = 64 });

            db.Products.Add(new Product
            { Name = "Конфеты Красный Октябрь сливочная помадка с цукатом", Description = "Сливочной помадке скоро исполнится 100 лет: этот десерт появился в 20-е годы прошлого века, и с тех пор не одно поколение москвичей выросло на нежной помадке с цукатами", 
                PhotoUrl = "product13", Price = 22 });

            db.Products.Add(new Product
            { Name = "Конфеты Акконд Птица дивная", Description = "Конфеты «Птица дивная» ? это очень нежное сливочное суфле, покрытое слоем вкусного тающего шоколада", PhotoUrl = "product14", Price = 42 });
        }
    }
}
