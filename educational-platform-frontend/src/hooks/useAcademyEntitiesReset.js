import { useEffect, useRef } from "react";
import { FilterAction } from "./useAcademyEntities";

const useAcademyEntitiesReset = (
  resetCourseId = null,
  resetSubjectId = null,
  resetFacultyId = null,
  universityId,
  facultyId,
  subjectId,
  dispatch = null
) => {
  const isFirstRender1 = useRef(true);
  const isFirstRender2 = useRef(true);
  const isFirstRender3 = useRef(true);

  useEffect(() => {
    if (isFirstRender1.current) {
      isFirstRender1.current = false;

      return;
    }

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
    if (isFirstRender2.current) {
      isFirstRender2.current = false;
      return;
    }

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
    if (isFirstRender3.current) {
      isFirstRender3.current = false;
      return;
    }
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
