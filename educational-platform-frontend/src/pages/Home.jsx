import ContentContainer from "../components/didactic/common/ContentContainer";

const Home = () => {
  return (
    <ContentContainer>
      <section className="content__section">
        <h2>Informacje o aplikacji</h2>
        <p>
          Strona powstała w celu udostępniania sobie wzajemnie materiałów
          dydaktycznych, a także różnych ćwiczeń. Po zalogowaniu się istnieje
          możliwość dodawania materiałów oraz ćwiczeń. Jeśli nie ma na liście
          dostępnych uczelni/wydziałów/kierunków/przedmiotów, które chcesz użyć
          dodaj prośbę o ich dodanie w profilu użytkownika. Ćwiczenia oraz
          materiały można oceniać w skali 1-5, a także komentować. Do ćwiczeń
          można także dodawać rozwiązania i komentować rozwiązania innych osób.
          Dane rozwiązanie także można ocenić w skali 1-5.
        </p>
      </section>
      <section className="content__section">
        <h2>Najlepiej ocenione materiały</h2>
      </section>
      <section className="content__section">
        <h2>Najlepiej ocenione ćwiczenia</h2>
      </section>
    </ContentContainer>
  );
};

export default Home;
