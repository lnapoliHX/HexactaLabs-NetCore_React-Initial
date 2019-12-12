import api from "../../common/api";
import { replace, go } from "connected-react-router";

const AUTH_REQUEST = "AUTH/REQUEST";
const AUTH_RESPONSE = "AUTH/RESPONSE";
const AUTH_ERROR = "AUTH/ERROR";
const AUTH_CANCEL = "AUTH/CANCEL";
const AUTH_LOGOUT = "AUTH/LOGOUT";

const initialState = {
  loading: false,
  authenticated: false,
  errorMessage: null
};

export default function reducer(state = initialState, action = {}) {
  switch (action.type) {
    case AUTH_REQUEST:
      return { ...state, loading: true };
    case AUTH_RESPONSE:
      return { ...state, loading: false, authenticated: true };
    case AUTH_ERROR:
      return {
        ...state,
        loading: false,
        authenticated: false,
        errorMessage: action.error
      };
    case AUTH_CANCEL:
      return { ...state, loading: false, authenticated: false };
    case AUTH_LOGOUT:
      return state;
    default:
      return state;
  }
}

export const login = ({ username, password }) => dispatch => {
  dispatch({ type: AUTH_REQUEST });

  if (username === password && username === "admin") {
    localStorage.setItem("JWT_LOGIN", "ESTO ES UN JWT DE PRUEBA");
    dispatch(go("/"));
    dispatch({ type: AUTH_RESPONSE });
  } else {
    dispatch({
      type: AUTH_ERROR,
      error:
        "Las credenciales no son correctas, intente nuevamente. (admin,admin)"
    });
  }

  // const url = '/authenticate';

  // api.post(url, { username: username, password: password })
  //     .then((response, ...other) => {
  //         localStorage.setItem('JWT_LOGIN', response.data)
  //         dispatch(go('/'))
  //         dispatch({ type: AUTH_RESPONSE, payload: response.data })
  //     })
  //     .catch(() => {
  //         //toast.error('Ocurrió un error');
  //         dispatch({ type: AUTH_ERROR, error: 'Las credenciales no son correctas, intente nuevamente.' })
  //     });
};

export const signup = ({ username, password }) => dispatch => {
  dispatch({ type: AUTH_REQUEST });

  const url = "/public/sign-up";

  api
    .post(url, { username: username, password: password })
    .then(response => dispatch(login({ username, password })))
    .catch(() => {
      //toast.error('Ocurrió un error');
      return dispatch({
        type: AUTH_ERROR,
        error: "Las credenciales no son correctas, intente nuevamente."
      });
    });
};

export const logout = () => dispatch => {
  localStorage.removeItem("JWT_LOGIN");
  dispatch(replace("/"));
  window.location.reload();
};
