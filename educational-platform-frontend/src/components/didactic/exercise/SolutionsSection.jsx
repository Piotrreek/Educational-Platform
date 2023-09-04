const SolutionsSection = ({ solutions }) => {
  return (
    <>
      <section className="content__section">
        <h2>Rozwiązania</h2>
        {!!solutions.length ? (
          <></>
        ) : (
          "To ćwiczenie nie posiada jeszcze żadnych rozwiązań. Zaloguj się i wyślij swoje"
        )}
      </section>
    </>
  );
};

export default SolutionsSection;
