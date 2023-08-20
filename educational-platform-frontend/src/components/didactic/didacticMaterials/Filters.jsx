import classes from "./Filters.module.css";
import { useLoaderData } from "react-router-dom";
import useInput from "../../../hooks/useInput";
import useCurrentAcademyEntitiesOptions from "../../../hooks/useCurrentAcademyEntitiesOptions";
import { FilterAction } from "../../../hooks/useMaterialFilters";
import useAcademyEntitiesReset from "../../../hooks/useAcademyEntitiesReset";

const Filters = ({ dispatch, filters }) => {
  const { reset: resetFacultyId } = useInput(() => true);
  const { reset: resetSubjectId } = useInput(() => true);
  const { reset: resetCourseId } = useInput(() => true);

  useAcademyEntitiesReset(
    resetCourseId,
    resetSubjectId,
    resetFacultyId,
    filters.universityId,
    filters.facultyId,
    filters.subjectId,
    dispatch
  );

  const onSubmit = (event) => {
    event.preventDefault();
  };

  const universities = useLoaderData().universityEntities;

  const { universityOptions, facultyOptions, subjectOptions, courseOptions } =
    useCurrentAcademyEntitiesOptions(
      universities,
      filters.universityId,
      filters.facultyId,
      filters.subjectId
    );

  const universityChangeHandler = (e) => {
    dispatch({
      field: FilterAction.universityId,
      payload: e.target.value,
    });
  };

  const facultyChangeHandler = (e) => {
    dispatch({
      field: FilterAction.facultyId,
      payload: e.target.value,
    });
  };

  const subjectChangeHandler = (e) => {
    dispatch({
      field: FilterAction.subjectId,
      payload: e.target.value,
    });
  };

  const courseChangeHandler = (e) => {
    dispatch({
      field: FilterAction.courseId,
      payload: e.target.value,
    });
  };

  return (
    <div className={classes.filters}>
      <h2>Materiały</h2>
      <form onSubmit={onSubmit} className={classes.form}>
        <div className={classes.filterSelect}>
          <label htmlFor="academy" className={classes.label}>
            Uczelnia
          </label>
          <select
            id="academy"
            value={filters.universityId}
            onChange={universityChangeHandler}
          >
            {universityOptions.map((university) => (
              <option key={university.value} value={university.value}>
                {university.text}
              </option>
            ))}
          </select>
        </div>
        <div className={classes.filterSelect}>
          <label htmlFor="faculty" className={classes.label}>
            Wydział
          </label>
          <select
            id="faculty"
            value={filters.facultyId}
            onChange={facultyChangeHandler}
          >
            {facultyOptions.map((faculty) => (
              <option key={faculty.value} value={faculty.value}>
                {faculty.text}
              </option>
            ))}
          </select>
        </div>
        <div className={classes.filterSelect}>
          <label htmlFor="subject" className={classes.label}>
            Kierunek
          </label>
          <select
            id="subject"
            value={filters.subjectId}
            onChange={subjectChangeHandler}
          >
            {subjectOptions.map((subject) => (
              <option key={subject.value} value={subject.value}>
                {subject.text}
              </option>
            ))}
          </select>
        </div>
        <div className={classes.filterSelect}>
          <label htmlFor="course" className={classes.label}>
            Przedmiot
          </label>
          <select
            id="course"
            value={filters.courseId}
            onChange={courseChangeHandler}
          >
            {courseOptions.map((course) => (
              <option key={course.value} value={course.value}>
                {course.text}
              </option>
            ))}
          </select>
        </div>
      </form>
    </div>
  );
};

export default Filters;
