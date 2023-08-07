import classes from "./Filters.module.css";
import Button from "../../ui/Button";

const Filters = () => {
  const onSubmit = (event) => {
    event.preventDefault();
  };

  return (
    <div className={classes.filters}>
      <h2>Materiały</h2>
      <form onSubmit={onSubmit}>
        <div className={classes.filterSelect}>
          <label htmlFor="academy">Uczelnia</label>
          <select id="academy">
            <option>Uczelnia</option>
          </select>
        </div>
        <div className={classes.filterSelect}>
          <label htmlFor="faculty">Wydział</label>
          <select id="faculty">
            <option>Wydział</option>
          </select>
        </div>
        <div className={classes.filterSelect}>
          <label htmlFor="subject">Kierunek</label>
          <select id="subject">
            <option>Kierunek</option>
          </select>
        </div>
        <div className={classes.filterSelect}>
          <label htmlFor="course">Przedmiot</label>
          <select id="course">
            <option>Przedmiot</option>
          </select>
        </div>
        <div className={classes.actions}>
          <Button>Filtruj</Button>
        </div>
      </form>
    </div>
  );
};

export default Filters;
