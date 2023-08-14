import classes from "./Material.module.css";

const OriginSection = ({ author, academy, faculty, subject, course }) => {
  return (
    <section className={classes.origin__info}>
      <h2>Pochodzenie materiału</h2>
      <p>
        Autor: <span>{author}</span>
      </p>
      <p>
        Uczelnia: <span>{academy}</span>
      </p>
      <p>
        Wydział: <span>{faculty}</span>
      </p>
      <p>
        Kierunek: <span>{subject}</span>
      </p>
      <p>
        Przedmiot: <span>{course}</span>
      </p>
    </section>
  );
};

export default OriginSection;
