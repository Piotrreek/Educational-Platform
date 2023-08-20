import { useEffect } from "react";
import { FilterAction } from "./useMaterialFilters";

const useAcademyEntitiesReset = (
  resetCourseId = null,
  resetSubjectId = null,
  resetFacultyId = null,
  universityId,
  facultyId,
  subjectId,
  dispatch = null
) => {
  useEffect(() => {
    !!resetCourseId && resetCourseId();
    !!resetSubjectId && resetSubjectId();
    !!resetFacultyId && resetFacultyId();

    if (!!dispatch) {
      dispatch({
        field: FilterAction.courseId,
        payload: "",
      });
      dispatch({
        field: FilterAction.subjectId,
        payload: "",
      });
      dispatch({
        field: FilterAction.facultyId,
        payload: "",
      });
    }
  }, [universityId, resetFacultyId, resetCourseId, resetSubjectId, dispatch]);

  useEffect(() => {
    !!resetCourseId && resetCourseId();
    !!resetSubjectId && resetSubjectId();

    if (!!dispatch) {
      dispatch({
        field: FilterAction.courseId,
        payload: "",
      });
      dispatch({
        field: FilterAction.subjectId,
        payload: "",
      });
    }
  }, [facultyId, resetSubjectId, resetCourseId, dispatch]);

  useEffect(() => {
    !!resetCourseId && resetCourseId();

    if (!!dispatch) {
      dispatch({
        field: FilterAction.courseId,
        payload: "",
      });
    }
  }, [subjectId, resetCourseId, dispatch]);
};

export default useAcademyEntitiesReset;
